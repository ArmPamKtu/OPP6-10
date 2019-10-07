using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public abstract class Player : Unit
    {
        public int Money { get; set; }
        public int NumberOfActions { get; set; }
        public string Name { get; set; }
        public string Faction { get; set; }

        public Player(string faction)
        {
            Money = 0;
            NumberOfActions = 7;
            Faction = faction;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        public void Creation()
        {
            Console.WriteLine("You chose to be a " + Faction);
        }
    }
}
