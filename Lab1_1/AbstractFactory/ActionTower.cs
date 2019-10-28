using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    class ActionTower: SuperObstacle
    {
        public ActionTower(int x, int y) : base(x, y)
        {
            symbol = 'A';
            
        }

        public override void Update()
        {
            //Player has to get more moves
            //owner.NumberOfActions++;
        }
    }
}
