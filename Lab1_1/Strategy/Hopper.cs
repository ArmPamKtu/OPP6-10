using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Hopper : Algorithm
    {
        public override void Action(Player player, string command, Map map, bool undo)
        {
            player.SetPreviousCoordinates(player.currentX, player.currentY);
            player.Power = 2;
            switch (command)
            {
                case "U":
                    if (player.currentY - player.Power > -1)
                        player.currentY -= player.Power;
                    player.Money = player.Money + player.MoneyMultiplier;
                    break;
                case "R":
                    if (player.currentX + player.Power < map.GetXSize())
                        player.currentX += player.Power;
                    player.Money = player.Money + player.MoneyMultiplier;
                    break;
                case "D":
                    if (player.currentY + player.Power < map.GetYSize())
                        player.currentY += player.Power;
                    player.Money = player.Money + player.MoneyMultiplier;
                    break;
                case "L":
                    if (player.currentX - player.Power > -1)
                        player.currentX -= player.Power;
                    player.Money = player.Money + player.MoneyMultiplier;
                    break;
                default:
                    map.GetUnit(player.currentX, player.currentY).ResetSymbol();
                    player.currentX = 0;
                    player.currentY = 0;
                    break;
            }

           
            map.GetUnit(player.GetPreviousX(), player.GetPreviousY()).ResetSymbol();
            map.GetUnit(player.currentX, player.currentY).TakeUnit(player);

        }

    }
}
