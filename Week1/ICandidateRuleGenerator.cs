using System;
using System.Collections.Generic;

namespace DataMining
{
    public interface ICandidateRuleGenerator<T>
    {
        List<AssociationRule<T>> GenerateCandidateRules(List<IFact<T>> targetFacts, List<ItemSet<IFact<T>>> frequentPatterns);
    }
}
