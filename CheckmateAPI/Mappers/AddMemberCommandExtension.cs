using CheckmateAPI.Commands.Member;
using Checkmate.DAL.Entities;

namespace CheckmateAPI.Mappers
{
    public static class AddMemberCommandExtension
    {
        public static Member ToEntity(this AddMemberCommand cmd, byte[] pwdHashed, Guid salt)
        {
            Member newMember = new Member
            {
                Nickname = cmd.Nickname,
                Email = cmd.Email,
                BirthDate = cmd.BirthDate,
                Gender = cmd.Gender,
                Elo = (int)((cmd.Elo != null) ? cmd.Elo : 1200),
                EncodedPassword = pwdHashed,
                Role = RoleType.Other,
                Salt = salt
            };
            return newMember;
        }
    }
}
