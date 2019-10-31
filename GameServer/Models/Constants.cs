using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Constants
    {
        public const int playerCount = 4;
        public const int mapLenghtX = 4;
        public const int mapLenghtY = 4;

        public static List<(int, int)> cordsMinusY = new List<(int, int)>
        {
            (-1,0), (1,0), (-1,1), (0,1), (1,1)
        };
        public static List<(int, int)> cordsMinusX = new List<(int, int)>
        {
            (0,-1), (0,1), (1,-1), (1,0), (1,1)
        };
        public static List<(int, int)> cordsPlusY = new List<(int, int)>
        {
            (-1,0), (1,0), (-1,-1), (0,-1), (1,-1)
        };
        public static List<(int, int)> cordsPlusX = new List<(int, int)>
        {
            (-1,1), (-1,0), (-1,1), (0,-1), (0,1)
        };
        public static List<(int, int)> cords = new List<(int, int)>
        {
            (-1,0), (-1,1), (1,-1), (0,-1), (0,1), (1,0), (1,1), (-1,-1)
        };
    }
}
