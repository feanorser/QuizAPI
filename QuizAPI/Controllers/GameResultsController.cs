using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultsController : ControllerBase
    {
        private readonly QuizDataBaseContext _context;

        public GameResultsController(QuizDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/GameResults/5
        [HttpGet("{gameId}")]
        public async Task<ActionResult<IEnumerable<GameResult>>> GetGameResult(Guid gameId)
        {
            var gameResult = await _context.GamesResults.Where(gr=> gr.GameId == gameId).Include(gr=>gr.Team).ToListAsync();

            if (gameResult == null)
            {
                return NotFound();
            }

            return gameResult;
        }

        // GET: api/GameResults/5/Team/12
        [HttpGet("{gameId}/Team/{teamId}")]
        public async Task<ActionResult<GameResult>> GetGamesResult(Guid gameId, Guid teamId)
        {
            var gameResult = await _context.GamesResults.FindAsync(gameId, teamId);

            if (gameResult == null)
            {
                return NotFound();
            }

            return gameResult;
        }

        // PUT: api/GameResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{gameId}")]
        public async Task<IActionResult> PutGameResult(Guid gameId, GameResult gameResult)
        {
            if (gameId != gameResult.GameId)
            {
                return BadRequest();
            }

            _context.Entry(gameResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameResultExists(gameId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GameResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameResult>> PostGameResult(GameResult gameResult)
        {
            _context.GamesResults.Add(gameResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GameResultExists(gameResult.GameId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGameResult", new { id = gameResult.GameId }, gameResult);
        }

        // DELETE: api/GameResults/5/Team/12
        [HttpDelete("{gameId}/Team/{teamId}")]
        public async Task<IActionResult> DeleteGameResult(Guid gameId, Guid teamId)
        {
            var gameResult = await _context.GamesResults.FindAsync(gameId, teamId);
            if (gameResult == null)
            {
                return NotFound();
            }

            _context.GamesResults.Remove(gameResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameResultExists(Guid id)
        {
            return _context.GamesResults.Any(e => e.GameId == id);
        }
    }
}
