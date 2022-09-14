using Checkmate.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace CheckmateAPI.Commands.Member
{
    public class AddMemberCommand
    {

        [Required, MinLength(2), MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Range(0, 3000)]
        public int? Elo { get; set; }
    }
}
