using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WindowsFormsApp2.DecoratorPattern
{
    public class Decorator : IBrush
    {
        public Cell _cell;
        public Decorator(Cell c) {
            _cell = c;
        }
        public void SetBrush(TextureBrush _tb) { _cell.tb = _tb; }
    }
}
