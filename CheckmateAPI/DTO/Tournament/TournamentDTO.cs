using Checkmate.DAL.Entities;
using CheckmateAPI.DTO.Member;

namespace CheckmateAPI.DTO.Tournament
{
    public class TournamentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Location { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int? MinElo { get; set; }

        public int? MaxElo { get; set; }

        public IEnumerable<TournamentCategoryType> Category { get; set; }

        public TournamentStatusType Status { get; set; }

        public int Round { get; set; }

        public bool WomenOnly { get; set; }

        public DateTime RegistrationEndDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public IEnumerable<MemberDTO>? Players { get; set; }
    }
}
