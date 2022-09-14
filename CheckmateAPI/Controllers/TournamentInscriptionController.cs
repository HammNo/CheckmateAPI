using Checkmate.DAL.Entities;
using CheckmateAPI.DTO.Member;
using CheckmateAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CheckmateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentInscriptionController : ControllerBase
    {

        private readonly MemberService _memberService;
        private readonly TournamentService _tournamentService;

        public TournamentInscriptionController(MemberService memberService, TournamentService tournamentService)
        {
            _memberService = memberService;
            _tournamentService = tournamentService;
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Other")]
        public IActionResult Register([FromRoute] int id)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Member member = _memberService.Get(int.Parse(userId));
                _tournamentService.Register(id, member);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpHead("CanParticipate/{id}")]
        [Authorize(Roles = "Other")]
        public IActionResult CanParticipate([FromRoute] int id)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Member member = _memberService.Get(int.Parse(userId));
                if (!_tournamentService.CanParticipateRequest(id, member)) return BadRequest();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Other")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Member member = _memberService.Get(int.Parse(userId));
                _tournamentService.Unregister(id, member);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
