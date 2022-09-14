using CheckmateAPI.Commands.Tournament;
using CheckmateAPI.DTO.Tournament;
using CheckmateAPI.Mappers;
using CheckmateAPI.Queries;
using Checkmate.DAL;
using Checkmate.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckmateAPI.Services
{
    public class TournamentService
    {
        private readonly CheckmateContext _checkmateContext;
        private readonly MailService _mailService;

        public TournamentService(CheckmateContext checkmateContext, MailService mailService)
        {
            _checkmateContext = checkmateContext;
            _mailService = mailService;
        }

        public IEnumerable<TournamentDTO> Get(GetTournamentQuery query)
        {
            int take = 10;
            if (query.Take != null) take = (int)query.Take; 
            IEnumerable<TournamentDTO> tournaments 
                = _checkmateContext.Tournaments.Include(t => t.Members)
                                               .ToList()
                                               .Where(t => t.Name.ToLower()
                                                                 .Contains((query.SearchedName != null)? query.SearchedName.ToLower() : ""))
                                               .Take(take)
                                               .Select(t => (new TournamentDTO()).toDTO(t));
            return tournaments;
        }

        public TournamentDTO? GetById(int id)
        {
            Tournament? tournament
                = _checkmateContext.Tournaments.Include(t => t.Members)
                                               .ToList()
                                               .SingleOrDefault(t => t.Id == id);
            if (tournament == null) throw new ArgumentException();
            return new TournamentDTO().toDTO(tournament);
        }

        public void Add(AddTournamentCommand cmd)
        {
            if (cmd.MinPlayers > cmd.MaxPlayers) throw new ArgumentException("Le nombre min de joueurs doit être inférieur ou égal au nombre max");
            if (cmd.MinElo > cmd.MaxElo) throw new ArgumentException("L'Elo min doit être inférieur ou égal au Elo max");
            if (cmd.TimeToRegister < cmd.MinPlayers) throw new ArgumentException("La date de fin des inscriptions doit être supérieure à la date de création + nombre de joueurs");
            if (cmd.MinPlayers < 2 || cmd.MinPlayers > 32) throw new ArgumentException("Le nombre min de joueurs doit être compris entre 2 et 32");
            if (cmd.MaxPlayers < 2 || cmd.MaxPlayers > 32) throw new ArgumentException("Le nombre max de joueurs doit être compris entre 2 et 32");
            if (cmd.MinElo < 0 || cmd.MinElo > 3000) throw new ArgumentException("L'Elo min doit être compris entre 0 et 3000");
            if (cmd.MaxElo < 0 || cmd.MaxElo > 3000) throw new ArgumentException("L'Elo max doit être compris entre 0 et 3000");
            try
            {
                Tournament newTournament = cmd.ToEntity();
                _checkmateContext.Tournaments.Add(newTournament);
                _checkmateContext.SaveChanges();

                //Mailchain sending desactivated because no working SMTP provider

                //IEnumerable<string> potPlayerMails 
                //    = _checkmateContext.Members
                //                       .ToList()
                //                       .Where(m => CanParticipate(newTournament, m))
                //                       .Select(m => m.Email);

                //_mailService.SendTournanementAlert(newTournament, potPlayerMails.ToArray());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            Tournament? toDelete = _checkmateContext.Tournaments.SingleOrDefault(t => t.Id == id);
            if (toDelete == null) throw new KeyNotFoundException();
            if (toDelete.Status != TournamentStatusType.WaitingForPlayers) throw new Exception("Impossible de supprimer un tournoi déjà commencé");
            _checkmateContext.Tournaments.Remove(toDelete);
            _checkmateContext.SaveChanges();
        }

        public void Register(int tournamentId, Member member)
        {
            if(member == null) throw new ArgumentNullException("member");
            try
            {
                Tournament tournament = GetSingleTournament(tournamentId);
                if (!CanParticipate(tournament, member)) throw new Exception("Le membre ne respecte pas les conditions de participation");
                tournament.Members.Add(member);
                _checkmateContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Unregister(int tournamentId, Member member)
        {
            if (member == null) throw new ArgumentNullException("member");
            try
            {
                Tournament tournament = GetSingleTournament(tournamentId);
                if (tournament.Status != TournamentStatusType.WaitingForPlayers) throw new Exception("Le tournoi a déjà commencé");
                if (tournament.Members.SingleOrDefault(m => m == member) == null) throw new ArgumentException();
                tournament.Members.Remove(member);
                _checkmateContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CanParticipateRequest(int tournamentId, Member member)
        {
            if (member == null) throw new ArgumentNullException("member");
            try
            {
                Tournament tournament = GetSingleTournament(tournamentId);
                if (!CanParticipate(tournament, member)) return false;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Tournament GetSingleTournament(int tournamentId)
        {
            Tournament? tournament
                = _checkmateContext.Tournaments.Include(t => t.Members)
                                               .ToList()
                                               .SingleOrDefault(t => t.Id == tournamentId);
            if (tournament == null) throw new KeyNotFoundException();
            return tournament;
        }

        private bool CanParticipate(Tournament tournament, Member member)
        {
            if (tournament.Status != TournamentStatusType.WaitingForPlayers) return false;
            if (tournament.Members.SingleOrDefault(m => m == member) != null) return false;
            if (tournament.Members.Count() >= tournament.MaxPlayers) return false;
            if (!(member.Elo >= tournament.MinElo && member.Elo <= tournament.MaxElo)) return false;
            if (tournament.WomenOnly && member.Gender != GenderType.Female) return false;
            DateTime zeroTime = new DateTime(1, 1, 1);
            DateTime tEndDate = tournament.RegistrationEndDate;
            DateTime mBirthDate = member.BirthDate;
            TimeSpan span = tEndDate - mBirthDate;
            int memberAge = (zeroTime + span).Year - 1;
            TournamentCategoryType memberCategory;
            switch (memberAge)
            {
                case < 18:
                    memberCategory = TournamentCategoryType.Junior;
                    break;
                case < 60:
                    memberCategory = TournamentCategoryType.Senior;
                    break;
                default:
                    memberCategory = TournamentCategoryType.Veteran;
                    break;
            }
            if (!tournament.Category.HasFlag(memberCategory)) return false;
            return true;
        }
    }
}
