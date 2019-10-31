using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class Wonder: SuperObstacle
    {
        public Wonder(int x, int y):base(x, y)
        {
            symbol = 'W';
        }

        public override void Update(Map map, (int, int) cords, List<Unit> area)
        {
            //Player has to win
            //GameController.EndGame(owner)

            if (map.GetUnit(cords.Item1, cords.Item2).GetSymbol() == this.symbol)
                if (area.TrueForAll(item => item.color == area[0].color))
                    map.GetUnit(cords.Item1, cords.Item2).TakeUnit('0', area[0].color);
        }
    }
}
