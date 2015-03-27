using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1.Mocks
{
    public class MockRuleGenerator : ICandidateRuleGenerator<string>
    {
        public List<AssociationRule<string>> GenerateCandidateRules(List<IFact<string>> targetFacts, List<ItemSet<IFact<string>>> frequentPatterns)
        {
            //if (frequentPatterns.SequenceEqual(new List<ItemSet<IFact<string>>>() { new ItemSet<IFact<string>>(new MockFact("Correct!")) }))
            //{
            //}
            return new List<AssociationRule<string>>();

        }
    }
}
