using CheckmateAPI.Commands.Member;
using CheckmateAPI.DTO.Member;
using CheckmateAPI.Mappers;
using Checkmate.DAL;
using Checkmate.DAL.Entities;
using System.Security.Cryptography;
using System.Text;
using CheckmateAPI.Queries;

namespace CheckmateAPI.Services
{
    public class MemberService
    {
        private readonly CheckmateContext _checkmateContext;
        private readonly MailService _mailService;

        public MemberService(CheckmateContext checkmateContext, MailService mailService)
        {
            _checkmateContext = checkmateContext;
            _mailService = mailService;
        }

        public IEnumerable<MemberDTO> Get()
        {
            IEnumerable<MemberDTO> members 
                = _checkmateContext.Members
                    .Select(m => (new MemberDTO ()).toDTO(m));
            return members;
        }

        public Member Get(int id)
        {
            Member? member = _checkmateContext.Members.SingleOrDefault(m => m.Id == id);
            if(member == null) throw new KeyNotFoundException();
            return member;
        }

        public bool ExistsNickname(ExistsNicknameQuery query)
        {
            Member? existingMember = _checkmateContext.Members.SingleOrDefault(m => m.Nickname == query.Nickname);
            if(existingMember == null) return false;
            return true;
        }

        public void Add(AddMemberCommand cmd)
        {
            try
            {
                SHA512CryptoServiceProvider service = new SHA512CryptoServiceProvider();
                string rndpwd = Guid.NewGuid().ToString().Substring(0, 10);
                Guid guid = Guid.NewGuid();
                byte[] pwdToHash = Encoding.UTF8.GetBytes(rndpwd + guid.ToString());
                byte[] pwdHashed = service.ComputeHash(pwdToHash);

                Member newMember = cmd.ToEntity(pwdHashed, guid);

                _checkmateContext.Members.Add(newMember);
                _checkmateContext.SaveChanges();
                _mailService.SendMemberSub(newMember, rndpwd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            Member? toDelete = _checkmateContext.Members.SingleOrDefault(m => m.Id == id);
            if (toDelete == null) throw new KeyNotFoundException();
            _checkmateContext.Members.Remove(toDelete);
            _checkmateContext.SaveChanges();
        }

        public void ChangePassword(ChangePasswordCommand cmd, Member member)
        {
            try
            {
                if (member == null) throw new KeyNotFoundException();
                SHA512CryptoServiceProvider service = new SHA512CryptoServiceProvider();
                byte[] cmdPwdToHash = Encoding.UTF8.GetBytes(cmd.OldPassword + member.Salt);
                byte[] cmdPwdHashed = service.ComputeHash(cmdPwdToHash);
                if (!cmdPwdHashed.SequenceEqual(member.EncodedPassword)) throw new Exception("L'ancien mot de passe est incorrect");
                byte[] cmdNewPwdToHash = Encoding.UTF8.GetBytes(cmd.NewPassword + member.Salt);
                byte[] cmdNewPwdHashed = service.ComputeHash(cmdNewPwdToHash);
                member.EncodedPassword = cmdNewPwdHashed;
                _checkmateContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
