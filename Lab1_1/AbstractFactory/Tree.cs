using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class Tree: Obstacle
    {
        public Tree(int x, int y) : base(x, y)
        {
            symbol = 'T';
        }
        public override void ResetSymbol()
        {
            symbol = 'T';
        }
        public override void Update(Map map, (int, int) cords, List<Unit> area)
        {
            //Tree has to disappear and player has to take this unit
            //TakeUnit('0', player.GetColor(), player);

            if(map.GetUnit(cords.Item1, cords.Item2).GetSymbol()==this.symbol)
                if (area.TrueForAll(item => item.color == area[0].color))
                    map.GetUnit(cords.Item1, cords.Item2).TakeUnit('0', area[0].color);
        }
    }
}
