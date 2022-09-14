using Checkmate.DAL.Entities;

namespace CheckmateAPI.DTO.Member
{
    public class MemberDTO
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderType Gender { get; set; }

        public int Elo { get; set; }

        public RoleType Role { get; set; }
    }
}
