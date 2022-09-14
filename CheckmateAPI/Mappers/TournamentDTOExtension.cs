using CheckmateAPI.DTO.Tournament;
using Checkmate.DAL.Entities;
using CheckmateAPI.DTO.Member;

namespace CheckmateAPI.Mappers
{
    public static class TournamentDTOExtension
    {
        public static TournamentDTO toDTO(this TournamentDTO dto, Tournament t)
        {
            dto.Id = t.Id;
            dto.Name = t.Name;
            dto.Location = t.Location;
            dto.MinPlayers = t.MinPlayers;
            dto.MaxPlayers = t.MaxPlayers;
            dto.MinElo = t.MinElo;
            dto.MaxElo = t.MaxElo;
            dto.Category = Enum.GetValues<TournamentCategoryType>().Where(v => t.Category.HasFlag(v));
            dto.Status = t.Status;
            dto.Round = t.Round;
            dto.WomenOnly = t.WomenOnly;
            dto.RegistrationEndDate = t.RegistrationEndDate;
            dto.CreationDate = t.CreationDate;
            dto.UpdateDate = t.UpdateDate;
            dto.Players = t.Members?.Select(m => (new MemberDTO()).toDTO(m));
            return dto;
        }
    }
}
