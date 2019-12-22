using Lab1_1.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.ChainOfResponse
{
    public abstract class Handler
    {
        protected Handler successor;

        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }
        public abstract void ProcessRequest(Unit request);
    }
}
