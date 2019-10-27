using Lab1_1.Observer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Unit : AObserver
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }

        public Unit()
        {
            Console.WriteLine("Instantiated - " + this.GetType());
        }

        public override void Update(Player p)
        {
            Console.WriteLine("Unit");
        }
    }
}
