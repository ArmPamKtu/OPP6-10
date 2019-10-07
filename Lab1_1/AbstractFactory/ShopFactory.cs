using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    class ShopFactory
    {
        public Unit CreateObstacle(string input)
        {
            return new Tree();
        }

        public Unit CreateSuperObstacle(string input)
        {
            return new ActionTower();
        }
    }
}
