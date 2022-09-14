using CheckmateAPI.Commands.Tournament;
using Checkmate.DAL.Entities;

namespace CheckmateAPI.Mappers
{
    public static class AddTournamentCommandExtension
    {
        public static Tournament ToEntity(this AddTournamentCommand cmd)
        {
            Tournament newTournament = new Tournament
            {
                Name = cmd.Name,
                Location = cmd.Location,
                MinPlayers = cmd.MinPlayers,
                MaxPlayers = cmd.MaxPlayers,
                MinElo = cmd.MinElo,
                MaxElo = cmd.MaxElo,
                Category = (TournamentCategoryType)cmd.Category.Sum(c => (int)c),
                WomenOnly = cmd.WomenOnly,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                RegistrationEndDate = DateTime.Now.AddDays(cmd.TimeToRegister),
                Round = 0,
                Status = TournamentStatusType.WaitingForPlayers
            };
            return newTournament;
        }
    }
}
