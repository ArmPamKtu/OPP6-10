using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class GoldMine: SuperObstacle
    {
        public GoldMine(int x, int y) : base(x, y)
        {
            symbol = 'G';
        }
        public override void ResetSymbol()
        {
            symbol = 'G';
        }

        public override void Update(Map map, (int, int) cords, List<Unit> area)
        {
            //Player has to get more gold
            //owner.MoneyMultiplier *= 2;

            if (map.GetUnit(cords.Item1, cords.Item2).GetSymbol() == this.symbol)
                if (area.TrueForAll(item => item.color == area[0].color))
                    map.GetUnit(cords.Item1, cords.Item2).TakeUnit('0', area[0].color);
        }
    }
}
