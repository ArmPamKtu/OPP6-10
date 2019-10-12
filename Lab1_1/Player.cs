using Lab1_1.Streategy;
using Lab1_1.Observer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public abstract class Player : MovementStrategy
    {
        public int Money { get; set; }
        public int NumberOfActions { get; set; }
        public string Name { get; set; }
        public string Faction { get; set; }
        public int Power { get; set; }

        public int currentX { get; set; }
        public int currentY { get; set; }

        public List<AObserver> observers = new List<AObserver>();

        public Player(string faction, string type) :  base(type) 
        {
            currentX = 0;
            currentY = 0;
            Power = 1;
            Money = 0;
            NumberOfActions = 7;
            Faction = faction;
        }

        public Player(string faction) :base("")
        {
            currentX = 0;
            currentY = 0;
            Power = 1;
            Money = 0;
            NumberOfActions = 7;
            Faction = faction;
        }

        public void Attach(AObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(AObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (AObserver o in observers)
            {
                o.Update(this);
            }
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
