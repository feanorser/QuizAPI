using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/Teams/")]
    [ApiController]
    public class TeamResultsController : ControllerBase
    {
        private readonly QuizDataBaseContext _context;

        public TeamResultsController(QuizDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Teams/5/Results
        [HttpGet("{id}/Results")]
        public async Task<ActionResult<IEnumerable<GameResult>>> GetGameResult(Guid teamId)
        {
            var gameResult = await _context.GamesResults.Where(gr => gr.TeamId == teamId).Include(gr => gr.Game).ToListAsync();

            if (gameResult == null)
            {
                return NotFound();
            }

            return gameResult;
        }
    }
}
