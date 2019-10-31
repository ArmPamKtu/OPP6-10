using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class State
    {
        public long id { get; set; }
        public string GameState { get; set; }
        public string Winner { get; set; }

        public State() { }
    }
}
