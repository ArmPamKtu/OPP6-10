using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models;
using Microsoft.AspNetCore.Mvc;

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

        // GET api/values
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
        private bool PlayerWithNameExists(string name)
        {
            return _context.Players.Any(e => e.Name == name);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
