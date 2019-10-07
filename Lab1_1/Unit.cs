using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Unit
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }

        public Unit()
        {
            Console.WriteLine("I am a " + this.GetType());
        }
    }
}
