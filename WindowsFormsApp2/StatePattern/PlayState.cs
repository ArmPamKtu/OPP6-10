using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2.StatePattern
{
    class PlayState : State
    {
        public PlayState(): base()
        {
        }

        public override void RenderElements()
        {
            l1.Visible = false;
            l2.Visible = false;
            tb1.Visible = false;
            tb2.Visible = false;
            bt1.Visible = false;
        }
    }
}
