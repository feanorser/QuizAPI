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
    public class GamesController : ControllerBase
    {
        private readonly QuizDataBaseContext _context;

        public GamesController(QuizDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Games/Upcoming
        /// <summary>
        /// Get list of Upcoming games.
        /// </summary>
        /// <returns>Games list.</returns>
        [HttpGet("Upcoming")]
        public async Task<ActionResult<IEnumerable<Game>>> GetUpComingGames()
        {
            return await _context.Games.Where(g=>g.DateTime > DateTime.UtcNow).ToListAsync();
        }
        /// <summary>
        /// Get all Past games.
        /// </summary>
        /// <returns>Games list.</returns>
        [HttpGet("Past")]
        public async Task<ActionResult<IEnumerable<Game>>> GetPastGames()
        {
            return await _context.Games.Where(g => g.DateTime <= DateTime.UtcNow).ToListAsync();
        }

        // GET: api/Games/5
        /// <summary>
        /// Get Game by it Id.
        /// </summary>
        /// <param name="id">Game ID</param>
        /// <returns>Game object.</returns>
        /// <response code="200">Game retrieved</response>
        /// <response code="404">Game not found</response>
        /// <response code="500">Oops! Can't lookup your game right now</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(Guid id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put Game with provided Id.
        /// </summary>
        /// <param name="id">Game Id.</param>
        /// <param name="game">Game object.</param>
        /// <returns>No content</returns>
        /// <response code="204">Game updated</response>
        /// <response code="400">Provided Id didn't much game.Id object</response>
        /// <response code="404">Cann't find game to update</response>
        /// <response code="500">Oops! Can't lookup your game right now</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(Guid id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add new game
        /// </summary>
        /// <param name="game">Game object</param>
        /// <returns>Added game</returns>
        /// <response code="201">Game added</response>
        /// <response code="500">Oops! Can't lookup your game right now</response>
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        /// <summary>
        /// Delete game with provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <response code="204">Game deleted</response>
        /// <response code="404">Game to delete not found</response>
        /// <response code="500">Oops! Can't lookup your game right now</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(Guid id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
