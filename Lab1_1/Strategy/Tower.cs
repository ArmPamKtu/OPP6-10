using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Tower : Algorithm
    {
        public override void Action(Player player, string command, Map map)
        {
            player.SetPreviousCoordinates(player.currentX, player.currentY);
            int n = 0;
            switch (command)
            {
                case "U":
                    while (player.currentY >= 0)
                    {

                        player.Money = player.Money + player.MoneyMultiplier;

                        map.GetUnit(player.currentX, player.currentY).TakeUnit(player);

                        if(n != 0)
                            map.GetUnit(player.currentX, player.currentY + 1).ResetSymbol();

                      
                        if (player.currentY >= 0)
                            player.currentY--;

                        n++;
                    }
                    player.currentY++;
                    break;
                case "R":
                    while (player.currentX < map.GetXSize())
                    {
                        player.Money = player.Money + player.MoneyMultiplier;

                        map.GetUnit(player.currentX, player.currentY).TakeUnit(player);

                        if (n != 0)
                            map.GetUnit(player.currentX - 1, player.currentY).ResetSymbol();

                        if (player.currentX <= map.GetXSize())
                            player.currentX++;

                        n++;
                    }
                    player.currentX--;
                    break;
                case "D":
                    while (player.currentY < map.GetYSize())
                    {
                        player.Money = player.Money + player.MoneyMultiplier;
                        
                        map.GetUnit(player.currentX, player.currentY).TakeUnit(player);

                        if (n != 0)
                            map.GetUnit(player.currentX, player.currentY - 1).ResetSymbol();

                        if (player.currentY <= map.GetYSize())
                            player.currentY++;

                        n++;
                    }
                    player.currentY--;
                    break;
                case "L":
                    while (player.currentX >= 0)
                    {
                        player.Money = player.Money + player.MoneyMultiplier;
                        
                        map.GetUnit(player.currentX, player.currentY).TakeUnit(player);

                        if (n != 0)
                            map.GetUnit(player.currentX + 1, player.currentY).ResetSymbol();

                        if (player.currentX >= 0)
                            player.currentX--;

                        n++;
                    }
                    player.currentX++;
                    break;
                default:
                    map.GetUnit(player.GetPreviousX(), player.GetPreviousY()).ResetSymbol();
                    player.currentX = 0;
                    player.currentY = 0;
                    break;
            }
        }
    }
}
