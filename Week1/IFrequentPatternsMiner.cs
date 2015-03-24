using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week1
{
    public interface IFrequentPatternsMiner<T>
    {
        List<ItemSet<IFact<T>>> Mine(Database<T> database, Double minsup);
        List<ItemSet<IFact<T>>> Mine(Database<T> projectedDatabase, Database<T> targetDatabase, Double minsup);
    }
}
