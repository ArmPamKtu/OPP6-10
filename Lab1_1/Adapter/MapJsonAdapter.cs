using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Adapter
{
    public class MapJsonAdapter
    {
        MapJson map;
        public List<Unit> ConvertJsonToList()
        {
            return map.ConvertArrayToList();
        }
        public void ConvertListToJson(List<Unit> unitList)
        {
            map.ConvertListToArray(unitList);
        }
    }
}
