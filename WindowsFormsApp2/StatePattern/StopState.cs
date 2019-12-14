using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp2.StatePattern
{
    class StopState : State
    {
        public StopState()
        {
            Constants.IsButtonActive = false;
        }

        public override void RenderElements()
        {
            bt1.Visible = false;
            tb1.Visible = false;
            l1.Visible = true;
            l1.Text = "Game ended";
        }
    }
}
