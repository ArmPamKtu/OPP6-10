using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.TemplateMethodPattern
{
    public class SquareMovement : AbstractStart
    {
        sealed public override void ExtraPositions(Map map, Player player)
        {
            if (player.currentX > 1 && player.currentY > 1)
            {
                if (map.GetUnit(player.currentX - 1, player.currentY - 1).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX - 1, player.currentY - 1).TakeUnit(player);
                    map.GetUnit(player.currentX - 1, player.currentY - 1).ResetSymbol();
                }

                if (map.GetUnit(player.currentX - 2, player.currentY - 1).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX - 2, player.currentY - 1).TakeUnit(player);
                    map.GetUnit(player.currentX - 2, player.currentY - 1).ResetSymbol();
                }

                if (map.GetUnit(player.currentX - 1, player.currentY - 2).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX - 1, player.currentY - 2).TakeUnit(player);
                    map.GetUnit(player.currentX - 1, player.currentY - 2).ResetSymbol();
                }

                if (map.GetUnit(player.currentX - 2, player.currentY - 2).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX - 2, player.currentY - 2).TakeUnit(player);
                    map.GetUnit(player.currentX - 2, player.currentY - 2).ResetSymbol();
                }

            }
            else if (player.currentX > 1 && player.currentY < 2)
            {
                if (map.GetUnit(player.currentX - 1, player.currentY + 1).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX - 1, player.currentY + 1).TakeUnit(player);
                    map.GetUnit(player.currentX - 1, player.currentY + 1).ResetSymbol();
                }

                if (map.GetUnit(player.currentX - 2, player.currentY + 1).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX - 2, player.currentY + 1).TakeUnit(player);
                    map.GetUnit(player.currentX - 2, player.currentY + 1).ResetSymbol();
                }

                if (map.GetUnit(player.currentX - 1, player.currentY + 2).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX - 1, player.currentY + 2).TakeUnit(player);
                    map.GetUnit(player.currentX - 1, player.currentY + 2).ResetSymbol();
                }

                if (map.GetUnit(player.currentX - 2, player.currentY + 2).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX - 2, player.currentY + 2).TakeUnit(player);
                    map.GetUnit(player.currentX - 2, player.currentY + 2).ResetSymbol();
                }
            }
            else if (player.currentX < 2 && player.currentY > 1)
            {
                if (map.GetUnit(player.currentX + 1, player.currentY - 1).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX + 1, player.currentY - 1).TakeUnit(player);
                    map.GetUnit(player.currentX + 1, player.currentY - 1).ResetSymbol();
                }

                if (map.GetUnit(player.currentX + 2, player.currentY - 1).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX + 2, player.currentY - 1).TakeUnit(player);
                    map.GetUnit(player.currentX + 2, player.currentY - 1).ResetSymbol();
                }

                if (map.GetUnit(player.currentX + 1, player.currentY - 2).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX + 1, player.currentY - 2).TakeUnit(player);
                    map.GetUnit(player.currentX + 1, player.currentY - 2).ResetSymbol();
                }

                if (map.GetUnit(player.currentX + 2, player.currentY - 2).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX + 2, player.currentY - 2).TakeUnit(player);
                    map.GetUnit(player.currentX + 2, player.currentY - 2).ResetSymbol();
                }
            }
            else
            {
                if (map.GetUnit(player.currentX + 1, player.currentY + 1).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX + 1, player.currentY + 1).TakeUnit(player);
                    map.GetUnit(player.currentX + 1, player.currentY + 1).ResetSymbol();
                }

                if (map.GetUnit(player.currentX + 2, player.currentY + 1).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX + 2, player.currentY + 1).TakeUnit(player);
                    map.GetUnit(player.currentX + 2, player.currentY + 1).ResetSymbol();
                }

                if (map.GetUnit(player.currentX + 1, player.currentY + 2).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX + 1, player.currentY + 2).TakeUnit(player);
                    map.GetUnit(player.currentX + 1, player.currentY + 2).ResetSymbol();
                }

                if (map.GetUnit(player.currentX + 2, player.currentY + 2).GetSymbol().Equals('0'))
                {
                    map.GetUnit(player.currentX + 2, player.currentY + 2).TakeUnit(player);
                    map.GetUnit(player.currentX + 2, player.currentY + 2).ResetSymbol();
                }
            }

        }
    }
}
