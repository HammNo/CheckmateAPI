using CheckmateAPI.Commands.Member;
using CheckmateAPI.DTO.Member;
using CheckmateAPI.Services;
using Checkmate.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CheckmateAPI.Queries;
using System.Security.Claims;

namespace CheckmateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberService _memberService;

        public MemberController(MemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet()]
        [Authorize(Roles = "Admin")]
        [Produces(typeof(IEnumerable<MemberDTO>))]
        public IActionResult Get()
        {
            return Ok(_memberService.Get());
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] AddMemberCommand cmd)
        {
            try
            {
                _memberService.Add(cmd);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _memberService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpHead("ExistsNickname")]
        [Authorize(Roles = "Admin")]
        public IActionResult ExistsNickname([FromQuery] ExistsNicknameQuery query)
        {
            if(_memberService.ExistsNickname(query) == true) return NoContent();
            else return BadRequest();
        }

        [HttpPatch("Password")]
        [Authorize(Roles = "Admin,Other")]
        public IActionResult ChangePassword([FromBody] ChangePasswordCommand cmd)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Member member = _memberService.Get(int.Parse(userId));
                _memberService.ChangePassword(cmd, member);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
