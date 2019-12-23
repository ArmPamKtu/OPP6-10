using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Iterator
{
    public interface IIterator
    {
        void First();
        bool IsDone();
        void Next();
        Object CurrentItem();
    }
}
