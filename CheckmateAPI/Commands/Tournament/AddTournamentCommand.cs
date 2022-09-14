using Checkmate.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace CheckmateAPI.Commands.Tournament
{
    public class AddTournamentCommand
    {
        [Required]
        public string Name { get; set; }

        public string? Location { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int? MinElo { get; set; }

        public int? MaxElo { get; set; }

        public IEnumerable<TournamentCategoryType> Category { get; set; }

        public bool WomenOnly { get; set; }

        public int TimeToRegister { get; set; }

    }
}
