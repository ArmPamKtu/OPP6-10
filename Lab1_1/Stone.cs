using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    class Stone : Obstacle
    {
        public override void Update(Player p)
        {
            Console.WriteLine("Stone, player cords {0,5} {1,5}",p.currentX,p.currentY);
        }
    }
}
