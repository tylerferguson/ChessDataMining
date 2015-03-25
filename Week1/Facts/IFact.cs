using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week1
{
    public interface IFact<T>
    {
        bool IsTrue(T transaction);
        int CompareTo(IFact<T> that);

        bool Implies(IFact<T> fact1);
    }
}
