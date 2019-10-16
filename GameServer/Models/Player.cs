using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public int NumberOfActions { get; set; }
        public int PosX { get; set; }
        public long PosY { get; set; }

        public Player()
        {
        }
    }
}
