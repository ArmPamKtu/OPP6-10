using System;
using System.Collections.Generic;
using System.Text;
using Lab1_1.Streategy;

namespace Lab1_1.TemplateMethodPattern
{
    public abstract class AbstractStart
    {
        public virtual void GiveStartingBonus(Player player)
        {
            AlgorithmFactory algorithmFactory = new AlgorithmFactory();
            Algorithm standart = algorithmFactory.GetDefault("Standart");


            player.Money += 5;
            player.Faction = player.Faction + " Faction";
            player.setAlgorithm(standart);
        }

        public abstract void ExtraPositions(Map map, Player player);
        

        public void GameStart(Map map, Player player)
        {
            GiveStartingBonus(player);
            ExtraPositions(map, player);
        }

    }
}
