using System;
using System.Collections.Generic;

namespace Week1
{
    public interface ICandidateRuleGenerator<T>
    {
        List<AssociationRule<T>> GenerateCandidateRules(List<IFact<T>> targetFacts, List<ItemSet<IFact<T>>> frequentPatterns);
    }
}
