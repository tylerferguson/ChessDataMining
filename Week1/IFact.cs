using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week1
{
    public interface IFact<T>
    {
        bool isTrue(T transaction);
        int CompareTo(IFact<T> that);
    }
}
