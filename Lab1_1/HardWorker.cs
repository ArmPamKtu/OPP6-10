using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class HardWorker : Player
    {
        public HardWorker(string faction) : base(faction)
        {
            Money = 10;
            NumberOfActions = 8;
        }
    }
    //gali tureti savo specialu ejima ar galia, kad unikalumas butu
}
