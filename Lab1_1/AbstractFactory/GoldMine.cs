using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class GoldMine: SuperObstacle
    {
        public override void Update()
        {
            Console.WriteLine("Gold mine");
        }
    }
}
