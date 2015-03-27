using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1.Mocks
{
    public class MockThresholdFilterer : IThresholdFilterer<string>
    {
        public List<AssociationRule<string>> FilterByMinThresholds(List<IFact<string>> targetFacts, Database<string> projectedDatabase, List<ItemSet<IFact<string>>> frequentPatterns, List<AssociationRule<string>> candidateRules, double relativeMinsup, double minconf)
        {
            return candidateRules;
        }
    }
}
