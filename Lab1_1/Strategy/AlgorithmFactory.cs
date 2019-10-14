using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class AlgorithmFactory
    {
        public Algorithm GetDefault(string type)
        {
            switch (type)
            {
                case "Hopper":
                    return new Hopper();
                    break;
                case "Tower":
                    return new Tower();
                    break;
                case "Teleport":
                    return new Teleport();
                    break;
                case "Standart":
                    return new Standart();
                    break;
                default:
                    return null;

            }
            
        }
    }
}
