using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.DAL.Entities
{
    public class Member
    {
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string Nickname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public byte[] EncodedPassword { get; set; }

        [Required]
        public Guid Salt { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        [Range(0, 3000)]
        public int Elo { get; set; }

        [Required]
        public RoleType Role { get; set; }

        public ICollection<Tournament> Tournaments { get; set; }
    }
}
