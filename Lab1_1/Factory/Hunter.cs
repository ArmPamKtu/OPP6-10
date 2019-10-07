using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Hunter : Player
    {
        public Hunter(string faction) : base(faction)
        {
            Money = 10;
        }
        //gali tureti savo specialu ejima ar galia, kad unikalumas butu
    }
}
