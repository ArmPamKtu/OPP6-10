using Lab1_1.Observer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Unit : AObserver
    {
        protected char symbol = '0';
        protected ConsoleColor color { get; set; }
        protected Player owner { get; set; }
        protected int coordinateX { get; set; }
        protected int coordinateY { get; set; }

        public Unit(int x, int y)
        {
            color = ConsoleColor.White;
            owner = null;
            coordinateX = x;
            coordinateY = y;
        }

        public override void Update()
        {
            Console.WriteLine("Unit");
        }

        public void TakeUnit(Player owner)
        {
            this.symbol = owner.GetSymbol();
            this.color = owner.GetColor();
            this.owner = owner;
        }
        public void ResetSymbol()
        {
            symbol = '0';
        }

        public ConsoleColor GetColor()
        {
            return color;
        }

        public char GetSymbol()
        {
            return symbol;
        }
    }
}
