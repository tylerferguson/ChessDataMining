using System;
using System.Collections.Generic;

namespace Week1
{
    public interface IThresholdFilterer<T>
    {
        List<AssociationRule<T>> FilterByMinThresholds(List<IFact<T>> targetFacts, Database<T> projectedDatabase, List<ItemSet<IFact<T>>> frequentPatterns, List<AssociationRule<T>> candidateRules, double relativeMinsup, double minconf);
    }
}
