using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class Tree: Obstacle
    {
        public Tree(int x, int y) : base(x, y)
        {
            symbol = 'T';
        }

        public override void Update()
        {
            //Tree has to disappear and player has to take this unit
            //TakeUnit('0', player.GetColor(), player);
        }
    }
}
