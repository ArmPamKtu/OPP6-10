using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Adapter
{
    public class MapJsonAdapter : Map
    {
        MapJson map = new MapJson();
        public override List<Unit> ConvertArrayToList()
        {
            return map.ConvertJsonToList();
        }
        public override void ConvertListToArray(List<Unit> unitList)
        {
            map.ConvertListToJson(unitList);
        }
    }
}
