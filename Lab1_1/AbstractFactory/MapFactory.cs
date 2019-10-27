using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    class MapFactory
    {
        public Unit CreateObstacle(string input)
        {
            return new Stone();
        }

        public Unit CreateSuperObstacle(string input)
        {
            switch (input)
            {
                case "Wonder":
                    return new Wonder();
                case "Gold Mine":
                    return new GoldMine();
                default:
                    break;
            }
            return null;
        }
    }
}
