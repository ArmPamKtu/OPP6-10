using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Lab1_1.Adapter
{
    class MapJson : Map
    {
        private string mapJson;
        public override List<Unit> ConvertArrayToList()
        {
            List<Unit> unitList = JsonConvert.DeserializeObject<List<Unit>>(mapJson); 
            return unitList;
        }
        public override void ConvertListToArray(List<Unit> unitList)
        {
            mapJson = JsonConvert.SerializeObject(unitList);
        }
    }
}
