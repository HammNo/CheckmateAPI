using CheckmateAPI.DTO.Member;
using Checkmate.DAL.Entities;

namespace CheckmateAPI.Mappers
{
    public static class MemberDTOExtension
    {
        public static MemberDTO toDTO(this MemberDTO dto, Member m)
        {
            dto.Id = m.Id;
            dto.Nickname = m.Nickname;
            dto.Email = m.Email;
            dto.BirthDate = m.BirthDate;
            dto.Gender = m.Gender;
            dto.Elo = m.Elo;
            dto.Role = m.Role;
            return dto;
        }
    }
}
