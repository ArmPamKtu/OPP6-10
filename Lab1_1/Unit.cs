using Lab1_1.Observer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Unit : AObserver
    {
        protected char symbol = '0';
        protected string color { get; set; }
        protected Player owner { get; set; }
        protected int coordinateX { get; set; }
        protected int coordinateY { get; set; }

        public Unit(int x, int y)
        {
            color = "White";
            owner = null;
            coordinateX = x;
            coordinateY = y;
        }

        public override void Update()
        {
            Console.WriteLine("Unit");
        }

        public void TakeUnit(char symbol, string color, Player owner)
        {
            this.symbol = symbol;
            this.color = color;
            this.owner = owner;
        }

        public string GetColor()
        {
            return color;
        }

        public char GetSymbol()
        {
            return symbol;
        }
    }
}
