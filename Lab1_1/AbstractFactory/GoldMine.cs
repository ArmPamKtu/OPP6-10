using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class GoldMine: SuperObstacle
    {
        public GoldMine(int x, int y) : base(x, y)
        {
            symbol = 'G';
        }

        public override void Update()
        {
            //Player has to get more gold
            //owner.MoneyMultiplier *= 2;
        }
    }
}
