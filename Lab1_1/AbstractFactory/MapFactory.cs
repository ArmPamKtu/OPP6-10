using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    class MapFactory
    {
        public Unit CreateObstacle(string input, int x, int y)
        {
            return new Stone(x, y);
        }

        public Unit CreateSuperObstacle(string input, int x, int y)
        {
            switch (input)
            {
                case "Wonder":
                    return new Wonder(x, y);
                case "Gold Mine":
                    return new GoldMine(x, y);
                default:
                    return null;
            }
        }
    }
}
