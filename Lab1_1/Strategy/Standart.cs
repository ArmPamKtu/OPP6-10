﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Standart : Algorithm
    {
        public override void Action(Player player, string command, Map map)
        {
            player.SetPreviousCoordinates(player.currentX, player.currentY);

            player.Power = 1;
            switch (command)
            {
                case "U":
                    if (player.currentY + player.Power < map.GetYSize())
                        player.currentY += player.Power;
                    player.Money = player.Money + player.MoneyMultiplier;
                    break;
                case "R":
                    if (player.currentX + player.Power < map.GetXSize())
                        player.currentX += player.Power;
                    player.Money = player.Money + player.MoneyMultiplier;
                    break;
                case "D":
                    if (player.currentY - player.Power > -1)
                        player.currentY -= player.Power;
                    player.Money = player.Money + player.MoneyMultiplier;
                    break;
                case "L":
                    if (player.currentX - player.Power > -1)
                        player.currentX -= player.Power;
                    player.Money = player.Money + player.MoneyMultiplier;
                    break;
                default:
                    player.currentX = 0;
                    player.currentY = 0;
                    break;
            }

            map.GetUnit(player.currentX, player.currentY).TakeUnit(player);
            map.GetUnit(player.GetPreviousX(), player.GetPreviousY()).ResetSymbol();

        }
    }
}
