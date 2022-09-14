using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.DAL.Entities
{
    public class Tournament
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Location { get; set; }

        [Required]
        public int MinPlayers { get; set; }

        [Required]
        public int MaxPlayers { get; set; }

        public int? MinElo { get; set; }

        public int? MaxElo { get; set; }

        [Required]
        public TournamentCategoryType Category { get; set; }

        [Required]
        public TournamentStatusType Status { get; set; }

        [Required]
        public int Round { get; set; }

        [Required]
        public bool WomenOnly { get; set; }

        [Required]
        public DateTime RegistrationEndDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}
