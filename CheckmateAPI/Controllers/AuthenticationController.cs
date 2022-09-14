using CheckmateAPI.Commands.Authentication;
using CheckmateAPI.Services;
using Checkmate.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CheckmateAPI.DTO.Member;
using CheckmateAPI.Mappers;

namespace CheckmateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly LoginService _loginService;

        public AuthenticationController(TokenService tokenService, LoginService loginService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginCommand cmd)
        {
            try
            {
                Member? member = _loginService.Get(cmd);
                return Ok(
                    new
                    {
                        token = _tokenService.CreateToken(member),
                        role = member.Role,
                        user = (new MemberDTO()).toDTO(member)
                    }
                );

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
