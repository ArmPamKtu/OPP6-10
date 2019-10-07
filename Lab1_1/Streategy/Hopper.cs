using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Hopper : Algorithm
    {
        public override void Action(Player player, string command, int[][] map)
        {
            player.Power = 2;
            switch (command)
            {
                case "U":
                    if (player.currentY + player.Power < 10)
                        player.currentY += player.Power;
                    break;
                case "R":
                    if (player.currentX + player.Power < 10)
                        player.currentX += player.Power;
                    break;
                case "D":
                    if (player.currentY - player.Power > -1)
                        player.currentY -= player.Power;
                    break;
                case "L":
                    if (player.currentX - player.Power > -1)
                        player.currentX -= player.Power;
                    break;
                default:
                    player.currentX = 0;
                    player.currentY = 0;
                    break;
            }

            map[player.currentY][player.currentX] = 1;
        }

    }
}
