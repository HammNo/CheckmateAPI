using System.ComponentModel.DataAnnotations;

namespace CheckmateAPI.Commands.Member
{
    public class ChangePasswordCommand
    {
        [Required]
        public string OldPassword { get; set; }

        [Required, MinLength(5), MaxLength(20)]
        public string NewPassword { get; set; }
    }
}
