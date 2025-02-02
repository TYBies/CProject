using Microsoft.AspNetCore.Mvc;
using FootballMatches.API.Interfaces;
using FootballMatches.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace FootballMatches.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;        

        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
            
        }

        [HttpGet("matchdays")]
        public async Task<IActionResult> GetAvailableMatchDays()
        {
            var matchDays = await _matchService.GetAvailableMatchDaysAsync();
            return Ok(matchDays);
        }

        [HttpGet("matchday/{matchDay}")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatchesByMatchDay(int matchDay)
        {
            var matches = await _matchService.GetMatchesByMatchDayAsync(matchDay);
            return Ok(matches);
        }

        [HttpGet("daterange")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatchesByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var matches = await _matchService.GetMatchesByDateRangeAsync(start, end);
            return Ok(matches);
        }
    }
}