using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Tower : Algorithm
    {
        public override void Action(Player player, string command, int[][] map)
        {
            switch (command)
            {
                case "U":
                    while (player.currentY < 10)
                    {
                        player.Money = player.Money + player.MoneyMultiplier;
                        map[player.currentY][player.currentX] = 1;
                        if (player.currentY <= 9)
                            player.currentY++;    
                    }
                    player.currentY--;
                    break;
                case "R":
                    while (player.currentX < 10)
                    {
                        player.Money = player.Money + player.MoneyMultiplier;
                        map[player.currentY][player.currentX] = 1;
                        if(player.currentX <= 9)
                            player.currentX++;
                    }
                    player.currentX--;
                    break;
                case "D":
                    while (player.currentY >= 0)
                    {
                        player.Money = player.Money + player.MoneyMultiplier;
                        map[player.currentY][player.currentX] = 1;
                        if (player.currentY >= 0)
                            player.currentY--;
                       
                    }
                    player.currentY++;
                    break;
                case "L":
                    while (player.currentX >= 0)
                    {
                        player.Money = player.Money + player.MoneyMultiplier;
                        map[player.currentY][player.currentX] = 1;
                        if (player.currentX >= 0)
                            player.currentX--;
                       
                    }
                    player.currentX++;
                    break;
                default:
                    player.currentX = 0;
                    player.currentY = 0;
                    break;
            }
        }
    }
}
