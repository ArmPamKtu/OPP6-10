﻿using Lab1_1.CommandPattern;
using Lab1_1.Streategy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Player : MovementStrategy
    {
        public long id { get; set; }
        public int Money { get; set; }
        public int MoneyMultiplier { get; set; }
        public int NumberOfActions { get; set; }
        public string Name { get; set; }
        public string Faction { get; set; }
        public int Power { get; set; }

        private int previousX { get; set; }
        private int previousY { get; set; }

        private char symbol = '*';
        private ConsoleColor color { get; set; }
        public int currentX { get; set; }
        public int currentY { get; set; }



        private List<Command> _commands = new List<Command>();


        public Player(string faction) :base("")
        {
            currentX = 0;
            currentY = 0;
            MoneyMultiplier = 1;
            Power = 1;
            Money = 0;
            NumberOfActions = 7;


            Faction = faction;
            color = ConsoleColor.Yellow;
        }
        public Player() : base("")
        {
            color = ConsoleColor.Yellow;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        public void Creation()
        {
            Console.WriteLine("You chose to be a " + Faction);
        }
        public ConsoleColor GetColor()
        {
            return color;
        }
        public char GetSymbol()
        {
            return symbol;
        }
        public void SetPreviousCoordinates(int x, int y)
        {
            previousX = x;
            previousY = y;
        }

        public int GetPreviousX()
        {
            return previousX;
        }
        public int GetPreviousY()
        {
            return previousY;
        }
    }
    public class gamePlayer : Player
    {

    }
}
