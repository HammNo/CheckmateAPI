using System.ComponentModel.DataAnnotations;

namespace CheckmateAPI.Commands.Authentication
{
    public class LoginCommand
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
