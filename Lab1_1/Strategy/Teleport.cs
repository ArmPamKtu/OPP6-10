using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class Teleport : Algorithm
    {

        public override void Action(Player player, string command, Map map)
        {
            player.Power = 1;
            player.SetPreviousCoordinates(player.currentX, player.currentY);
            player.currentX = Int32.Parse(command[0].ToString());
            player.currentY = Int32.Parse(command[1].ToString());

            if (player.currentX > -1 && player.currentX < map.GetXSize() && player.currentY > -1 && player.currentY < map.GetYSize())
            {
                map.GetUnit(player.currentX, player.currentY).TakeUnit(player);
                player.Money = player.Money + player.MoneyMultiplier;
                map.GetUnit(player.GetPreviousX(), player.GetPreviousY()).ResetSymbol();
            }
        }
    }
}
