using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class FactionFactory : Factory
    {
       
        public override Player CreatePlayerWithFaction(string input)
        {
            switch (input)
            {
                case "H":
                    return new Hunter("Hunter");
                    break;
                case "W":
                    return new Wolf("Wolf");
                    break;
                case "HW":
                    return new HardWorker("Hard Worker");
                    break;
                default:
                    return null;

            }
        }
    }
}
