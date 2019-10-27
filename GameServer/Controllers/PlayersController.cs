using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameServer.Models;

namespace GameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerContext _context;

        public PlayersController(PlayerContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public IEnumerable<Player> GetPlayers()
        {
            return _context.Players;
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // PUT: api/Players/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer([FromRoute] long id, [FromBody] Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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
        // POST api/player
        [HttpPost]
        //public string Create(Player player)
        public ActionResult<Player> Create([FromBody] Player player)
        {
            if (!PlayerWithNameExists(player.Name))
            {
                _context.Players.Add(player);
                _context.SaveChanges();

                //return Ok(); //"created - ok"; 
                return CreatedAtRoute("GetPlayer", new { id = player.Id }, player);
            }
            else
                return null;
        }

        // GET api/player
        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetAll()
        {
            return _context.Players.ToList();
        }
        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }

        private bool PlayerExists(long id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
        private bool PlayerWithNameExists(string name)
        {
            return _context.Players.Any(e => e.Name == name);
        }
    }
}