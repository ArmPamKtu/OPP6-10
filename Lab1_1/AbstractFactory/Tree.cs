using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class Tree: Obstacle
    {
        public override void Update()
        {
            Console.WriteLine("Tree");
        }
    }
}
