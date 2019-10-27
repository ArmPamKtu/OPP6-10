using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab1_1.Observer;
using Lab1_1.AbstractFactory;
using GameServer.Models;

namespace GameServer.Controllers
{
    [Route("api/gamecontroller")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly PlayerContext _context;
        private readonly GameState gs;

        public GameController(PlayerContext context)
        {
            _context = context;
            gs = new GameState();

            gs.Attach(new Tree());
            gs.Attach(new SuperObstacle());

            //generate map
            _context.Map.Add(new TempMap { Map = 0 });
            _context.SaveChanges();
        }

        [HttpGet("{id}", Name ="GetMap")]
        public ActionResult<int> GetMap(long id)
        {
            var val = _context.PlayersGET_ID.ToList().Where(x => x.Id == id).FirstOrDefault();

            if (val == null)
            {
                Temp2 t = new Temp2 { Id = id };
                _context.PlayersGET_ID.Add(t);
                _context.SaveChanges();
            }

            List<Temp2> tl = _context.PlayersGET_ID.ToList();

            if (tl.Count() == Constants.playerCount)
            {
                _context.PlayersPOST_ID.RemoveRange(_context.PlayersPOST_ID);
                _context.SaveChanges();
            }

            return _context.Map.First().Map;
        }

        [HttpGet("/allp", Name = "GetAllPost")]
        public ActionResult<IEnumerable<Temp>> GetAllp()
        {
            return _context.PlayersPOST_ID.ToList();
        }

        [HttpGet("/allg", Name = "GetAllGet")]
        public ActionResult<IEnumerable<Temp2>> GetAllg()
        {
            return _context.PlayersGET_ID.ToList();
        }


        [HttpPost("{id}", Name = "UpdateMap")]
        public ActionResult<string> UpdatedMap(long id)
        {
            _context.PlayersPOST_ID.ToList();
            var val = _context.PlayersPOST_ID.ToList().Where(x => x.Id == id).FirstOrDefault();

            if (val==null)
            {
                Temp t = new Temp { Id = id };           
                _context.PlayersPOST_ID.Add(t);
                _context.SaveChanges();

                //merge maps

            }
            else if(val != null && _context.PlayersPOST_ID.ToList().Count() == Constants.playerCount)
            {
                return "Updated";
            }else
            {
                return "Updating";
            }

            if(_context.PlayersPOST_ID.ToList().Count() == Constants.playerCount)
            {
                _context.PlayersGET_ID.RemoveRange(_context.PlayersGET_ID);

                //update map
                _context.Map.First().Map += 1;
                gs.Notify();

                _context.SaveChanges();

                return "Updated";
            }
            else
            {
                return "Updating";
            }
        }
    }
}
