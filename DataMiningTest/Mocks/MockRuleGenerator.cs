using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Mocks
{
    public class MockRuleGenerator : ICandidateRuleGenerator<string>
    {
        public List<AssociationRule<string>> GenerateCandidateRules(IEnumerable<IFact<string>> targetFacts, List<ItemSet<IFact<string>>> frequentPatterns)
        {
            return new List<AssociationRule<string>>();

        }
    }
}
