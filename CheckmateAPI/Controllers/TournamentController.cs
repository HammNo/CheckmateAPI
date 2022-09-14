using CheckmateAPI.Commands.Tournament;
using CheckmateAPI.DTO.Tournament;
using CheckmateAPI.Queries;
using CheckmateAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckmateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly TournamentService _tournamentService;

        public TournamentController(TournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet()]
        [Produces(typeof(IEnumerable<TournamentDTO>))]
        public IActionResult Get([FromQuery] GetTournamentQuery query)
        {
            return Ok(_tournamentService.Get(query));
        }

        [HttpGet("GetById")]
        [Produces(typeof(IEnumerable<TournamentDTO>))]
        public IActionResult GetById([FromQuery] int id)
        {
            try
            {
                TournamentDTO? tournamentDTO = _tournamentService.GetById(id);
                return Ok(tournamentDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] AddTournamentCommand cmd)
        {
            try
            {
                _tournamentService.Add(cmd);
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
                _tournamentService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
