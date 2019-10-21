using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Teleport : Algorithm
    {

        public override void Action(Player player, string command, int[][] map)
        {
            player.Power = 1;
            player.currentX = Int32.Parse(command[0].ToString());
            player.currentY = Int32.Parse(command[1].ToString());

            if (player.currentX > -1 && player.currentX < map.Length && player.currentY > -1 && player.currentY < map.Length)
            {
                map[player.currentY][player.currentX] = 1;
                player.Money = player.Money + player.MoneyMultiplier;
            }
        }
    }
}
