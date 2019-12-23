using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Iterator
{
    public class MapIterator : IIterator
    {
        private Map map;
        private int xIndex;
        private int yIndex;

        public MapIterator(Map map)
        {
            this.map = map;
            xIndex = 0;
            yIndex = 0;
        }

        public void First()
        {
            xIndex = 0;
            yIndex = 0;
        }

        public bool IsDone()
        {
            if (xIndex == map.GetXSize() - 1 && yIndex == map.GetYSize() - 1)
                return true;
            return false;
        }

        public void Next()
        {
            if (xIndex < map.GetXSize() - 1)
            {
                xIndex++;
            }
            else if (xIndex >= map.GetXSize() - 1 && yIndex < map.GetYSize() - 1)
            {
                xIndex = 0;
                yIndex++;
            }
            else
            {
                return;
            }
        }

        public Object CurrentItem()
        {
            return map.GetUnit(xIndex, yIndex);
        }
    }
}
