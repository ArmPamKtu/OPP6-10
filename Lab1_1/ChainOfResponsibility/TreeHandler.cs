using Lab1_1.AbstractFactory;
using Lab1_1.ChainOfResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.ChainOfResponsibility
{
    class TreeHandler : Handler
    {
        public override void ProcessRequest(Unit request)
        {
            if (request.GetSymbol().Equals("T"))
            {
            }
            else if (successor != null)
            {
                successor.ProcessRequest(request);
            }
        }
    }
}
