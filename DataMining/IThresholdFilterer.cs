using System;
using System.Collections.Generic;

namespace DataMining
{
    public interface IThresholdFilterer<T>
    {
        List<AssociationRule<T>> FilterByMinThresholds(IEnumerable<IFact<T>> targetFacts, Database<T> projectedDatabase, List<ItemSet<IFact<T>>> frequentPatterns, List<AssociationRule<T>> candidateRules, double relativeMinsup, double minconf);
    }
}
