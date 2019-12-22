using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.TemplateMethodPattern
{
    public class CrossMovement : AbstractStart
    {
        sealed public override void ExtraPositions(Map map, Player player)
        {
            if(player.currentX > 1 && player.currentY > 1)
            {
                map.GetUnit(player.currentX - 2, player.currentY).TakeUnit(player);
                map.GetUnit(player.currentX - 3, player.currentY).TakeUnit(player);
                map.GetUnit(player.currentX, player.currentY - 2).TakeUnit(player);
                map.GetUnit(player.currentX, player.currentY - 3).TakeUnit(player);

                map.GetUnit(player.currentX - 2, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX - 3, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY - 2).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY - 3).ResetSymbol();
            }
            else if(player.currentX > 1 && player.currentY < 2)
            {
                map.GetUnit(player.currentX - 2, player.currentY).TakeUnit(player);
                map.GetUnit(player.currentX - 3, player.currentY).TakeUnit(player);
                map.GetUnit(player.currentX, player.currentY + 2).TakeUnit(player);
                map.GetUnit(player.currentX, player.currentY + 3).TakeUnit(player);

                map.GetUnit(player.currentX - 2, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX - 3, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY + 2).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY + 3).ResetSymbol();
            }
            else if(player.currentX < 2 && player.currentY > 1)
            {
                map.GetUnit(player.currentX + 2, player.currentY).TakeUnit(player);
                map.GetUnit(player.currentX + 3, player.currentY).TakeUnit(player);
                map.GetUnit(player.currentX, player.currentY - 2).TakeUnit(player);
                map.GetUnit(player.currentX, player.currentY - 3).TakeUnit(player);

                map.GetUnit(player.currentX + 2, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX + 3, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY - 2).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY - 3).ResetSymbol();
            }
            else
            {
                map.GetUnit(player.currentX + 2, player.currentY).TakeUnit(player);
                map.GetUnit(player.currentX + 3, player.currentY).TakeUnit(player);
                map.GetUnit(player.currentX, player.currentY + 2).TakeUnit(player);
                map.GetUnit(player.currentX, player.currentY + 3).TakeUnit(player);

                map.GetUnit(player.currentX + 2, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX + 3, player.currentY).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY + 2).ResetSymbol();
                map.GetUnit(player.currentX, player.currentY + 3).ResetSymbol();
            }
            
        }
    }
}
