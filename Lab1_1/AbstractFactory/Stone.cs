﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    class Stone: Obstacle
    {
        public Stone(int x, int y) : base(x, y)
        {
            symbol = 'S';
        }

        public override void Update()
        {
            //Nothing happens. A stone is a stone
        }
    }
}
