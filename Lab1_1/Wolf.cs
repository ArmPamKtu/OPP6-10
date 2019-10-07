using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Wolf : Player
    {
        public Wolf(string faction) : base(faction)
        {
            NumberOfActions = 9;
        }
        //gali tureti savo specialu ejima ar galia, kad unikalumas butu
    }
}
