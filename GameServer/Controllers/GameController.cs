using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab1_1.Observer;
using Lab1_1.AbstractFactory;
using GameServer.Models;
using Lab1_1;
using Newtonsoft.Json;

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

            gs.Attach(new Tree(0, 0));
            gs.Attach(new GoldMine(0, 0));
            gs.Attach(new ActionTower(0, 0));
            gs.Attach(new Wonder(0, 0));

            //Generate map
            if (_context.Map.ToList().Count < Constants.mapLenghtX * Constants.mapLenghtY)
            {
                Map.GetInstance.GenerateGrid(Constants.mapLenghtX, Constants.mapLenghtY);
                _context.State.Add(new State { StateGame = "Updating" });
                for (int y = 0; y < Map.GetInstance.GetYSize(); y++)
                {
                    for (int x = 0; x < Map.GetInstance.GetXSize(); x++)
                    {
                        Unit u = Map.GetInstance.GetUnit(x, y);
                        _context.Map.Add(JsonConvert.DeserializeObject<MapUnit>(JsonConvert.SerializeObject(u)));
                    }
                }
            }
            _context.SaveChanges();
        }

        [HttpGet("{id}", Name = "GetMap")]
        public ActionResult<List<MapUnit>> GetMap(long id)
        {
            var val = _context.PG_ID.ToList().Where(x => x.Id == id).FirstOrDefault();

            if (val == null)
            {
                PlayerGet t = new PlayerGet { Id = id };
                _context.PG_ID.Add(t);
                _context.SaveChanges();
            }

            List<PlayerGet> tl = _context.PG_ID.ToList();

            if (tl.Count() == Constants.playerCount)
            {
                _context.PP_ID.RemoveRange(_context.PP_ID);
                State s = _context.State.First();
                s.StateGame = "Updating";
                _context.SaveChanges();
            }

            return _context.Map.ToList();
        }

        [HttpGet("/allp", Name = "GetAllPost")]
        public ActionResult<IEnumerable<PlayerPost>> GetAllp()
        {
            return _context.PP_ID.ToList();
        }

        [HttpDelete]
        public IActionResult ResetGame()
        {
            _context.Map.RemoveRange(_context.Map);
            _context.Players.RemoveRange(_context.Players);
            _context.PP_ID.RemoveRange(_context.PP_ID);
            _context.PG_ID.RemoveRange(_context.PG_ID);
            _context.State.RemoveRange(_context.State);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("/allg", Name = "GetAllGet")]
        public ActionResult<IEnumerable<PlayerGet>> GetAllg()
        {
            return _context.PG_ID.ToList();
        }

        [HttpPost("{id}", Name = "UpdateMap")]
        public ActionResult</*List<MapUnit>*/State> UpdatedMap(long id, [FromBody] List<Unit> map)
        {
            Unit u;
            List<MapUnit> mp;

            var val = _context.PP_ID.ToList().Where(x => x.Id == id).FirstOrDefault();
            var playerc = _context.Players.Find(id);

            if (val == null)
            {
                mp = _context.Map.ToList();
                PlayerPost t = new PlayerPost { Id = id };
                _context.PP_ID.Add(t);

                //Merge maps
                Map.GetInstance.ConvertListToArray(map);
                for (int y = 0; y < Map.GetInstance.GetYSize(); y++)
                {
                    for (int x = 0; x < Map.GetInstance.GetXSize(); x++)
                    {
                        u = Map.GetInstance.GetUnit(x, y);
                        MapUnit mu = mp.ElementAt(y * Map.GetInstance.GetXSize() + x);

                        if (mu.symbol == '0' && u.GetSymbol() == '0' && mu.color == (ConsoleColor)15 && u.color != (ConsoleColor)15)
                        {
                            mu.color = u.GetColor();
                            mu.symbol = u.symbol;
                            _context.Map.Update(mu);
                        }//testavimui
                        else if (u.GetSymbol() == '*')
                        {
                            if (u.GetColor() != playerc.color)
                            {
                                mu.color = u.GetColor();
                                mu.symbol = '0';
                            }
                            else
                            {
                                mu.color = u.GetColor();
                                mu.symbol = u.symbol;
                            }
                            _context.Map.Update(mu);
                        }
                        else if (mu.symbol == '0' && u.GetSymbol() == '0' && mu.color != (ConsoleColor)15 && u.color != (ConsoleColor)15 && mu.color != u.color)
                        {
                            mu.color = (ConsoleColor)15;
                            //mu.ownerName = null;
                        //    _context.Map.Update(mu);
                        //}
                        //else
                        //{

                        //}

                        //if (mu.symbol == '*' && mu.color != playerc.color)
                        //{
                        //    mu.symbol = '0';
                            _context.Map.Update(mu);
                        }
                    }
                }
                _context.SaveChanges();
            }

            else if (_context.State.First().StateGame == "Updated" && _context.PP_ID.ToList().Count() == Constants.playerCount)
            {
                return _context.State.First();
                //return "Updated";
            }
            else
            {
                return _context.State.First();
                //return "Updating";
            }

            //return _context.State.First();

            if (_context.PP_ID.ToList().Count() == Constants.playerCount)
            {
                _context.PG_ID.RemoveRange(_context.PG_ID);
                List<Unit> area = new List<Unit>();
                List<Unit> unitmap = new List<Unit>();

                foreach (MapUnit mu in _context.Map.ToList())
                {
                    unitmap.Add(JsonConvert.DeserializeObject<Unit>(JsonConvert.SerializeObject(mu)));
                }
                Map.GetInstance.ConvertListToArray(unitmap);

                //Update map

                for (int y = 0; y < Map.GetInstance.GetYSize(); y++)
                {
                    for (int x = 0; x < Map.GetInstance.GetXSize(); x++)
                    {
                        if (Map.GetInstance.GetUnit(x, y).GetSymbol() != '0' && Map.GetInstance.GetUnit(x, y).GetSymbol() != '*')
                        {
                            if(x - 1 < 0 && y - 1 < 0 ||
                                x + 1 >= Map.GetInstance.GetXSize() && y - 1 < 0 ||
                                x - 1 < 0 && y + 1 >= Map.GetInstance.GetYSize() ||
                                x + 1 >= Map.GetInstance.GetXSize() && y + 1 >= Map.GetInstance.GetYSize())
                            {

                            }
                            else if (x - 1 < 0)
                            {
                                foreach ((int, int) t in Constants.cordsMinusX)
                                {
                                    u = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (u.symbol == '0' || u.symbol == '*')
                                    {
                                        area.Add(u);
                                    }
                                }
                            }
                            else if (x + 1 >= Map.GetInstance.GetXSize())
                            {
                                foreach ((int, int) t in Constants.cordsPlusX)
                                {
                                    u = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (u.symbol == '0' || u.symbol == '*')
                                    {
                                        area.Add(u);
                                    }
                                }
                            }
                            else if (y - 1 < 0)
                            {
                                foreach ((int, int) t in Constants.cordsMinusY)
                                {
                                    u = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (u.symbol == '0' || u.symbol == '*')
                                    {
                                        area.Add(u);
                                    }
                                }
                            }
                            else if (y + 1 >= Map.GetInstance.GetYSize())
                            {
                                foreach ((int, int) t in Constants.cordsPlusY)
                                {
                                    u = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (u.symbol == '0' || u.symbol == '*')
                                    {
                                        area.Add(u);
                                    }
                                }
                            }
                            else
                            {
                                foreach ((int, int) t in Constants.cords)
                                {
                                    u = Map.GetInstance.GetUnit(x + t.Item1, y + t.Item2);
                                    if (u.symbol == '0' || u.symbol == '*')
                                    {
                                        area.Add(u);
                                    }
                                }
                            }

                            if (area.FirstOrDefault() != null)
                            {
                                gs.Notify(Map.GetInstance, (x, y), area);
                                u = Map.GetInstance.GetUnit(x, y);
                                if (u != null && u.color != (ConsoleColor)15)
                                {
                                    //Update player
                                    Models.Player p = _context.Players.Where(player => player.color == u.color).FirstOrDefault();
                                    if (p != null && u.owner != null)
                                    {
                                        if (u.owner.NumberOfActions != 0)
                                            p.NumberOfActions += u.owner.NumberOfActions;
                                        if (u.owner.MoneyMultiplier != 0)
                                            p.MoneyMultiplier += u.owner.MoneyMultiplier;
                                        //if (u.GetSymbol() == 'L')
                                        //_context.State.First().Winner = u.GetColor();
                                        //_context.SaveChanges();
                                        _context.Players.Update(p);
                                        _context.SaveChanges();
                                    }
                                    if (u.GetSymbol() == 'L')
                                    {
                                        _context.State.First().Winner = u.color;
                                        _context.SaveChanges();
                                    }
                                }
                            }
                            area.Clear();
                        }
                    }
                }

                mp = _context.Map.ToList();
                List<Unit> ul = Map.GetInstance.ConvertArrayToList();
                for (int x = 0; x < mp.Count; x++)
                {
                    mp[x].color = ul[x].color;
                    mp[x].symbol = ul[x].symbol;
                    _context.Map.Update(mp[x]);
                }

                State s = _context.State.First();
                s.StateGame = "Updated";
                _context.SaveChanges();

                return _context.State.First();
            }
            else
            {
                return _context.State.First();
            }
        }
    }
}
