using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public abstract class MovementStrategy
    {
        private Algorithm _algorithm;
        private string _algorithmType;
        public MovementStrategy(string selectedType)
        {
            _algorithmType = selectedType;
            AlgorithmFactory factory = new AlgorithmFactory();
            Algorithm standart = factory.GetDefault(selectedType);
            setAlgorithm(standart);
        }

        public void move(Player player, string command, Map map)
        {
            _algorithm.Action(player, command, map);
        }
        public void setAlgorithm(Algorithm newAlgorithm)
        {
            _algorithm = newAlgorithm;
        }
    }
}
