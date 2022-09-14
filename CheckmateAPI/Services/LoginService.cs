using CheckmateAPI.Commands.Authentication;
using Checkmate.DAL;
using Checkmate.DAL.Entities;
using System.Security.Cryptography;
using System.Text;

namespace CheckmateAPI.Services
{
    public class LoginService
    {
        private readonly CheckmateContext _checkmateContext;

        public LoginService(CheckmateContext checkmateContext)
        {
            _checkmateContext = checkmateContext;
        }

        public Member? Get(LoginCommand cmd)
        {
            Member? member = _checkmateContext.Members.Where(m => m.Nickname == cmd.Nickname).FirstOrDefault();
            if (member == null) throw new ArgumentException();
            SHA512CryptoServiceProvider service = new SHA512CryptoServiceProvider();
            byte[] cmdPwdToHash = Encoding.UTF8.GetBytes(cmd.Password + member.Salt);
            byte[] cmdPwdHashed = service.ComputeHash(cmdPwdToHash);
            if (!cmdPwdHashed.SequenceEqual(member.EncodedPassword)) throw new Exception("Pseudo ou mot de passe incorrect");
            return (member);
        }
    }
}
