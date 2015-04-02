using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Mocks;
using Xunit;

namespace DataMining
{
    public class When_GenerateCandidateRules_is_called
    {
        [Fact]
        public void No_duplicate_candidate_rules_should_be_returned_for_maximal_freq_itemsets_that_overlap()
        {
            //Given
            IFact<string> factA;
            IFact<string> factB;
            IFact<string> factC;
            IFact<string> factD;

            factA = new MockFact("A");
            factB = new MockFact("B");
            factC = new MockFact("C");
            factD = new MockFact("D");

            var candidateRuleGenerator = new CandidateRuleGenerator<string>();

            var freqItemSets = new List<ItemSet<IFact<string>>>() 
            {
                new ItemSet<IFact<string>>(factA),
                new ItemSet<IFact<string>>(factB),
                new ItemSet<IFact<string>>(factC),
                new ItemSet<IFact<string>>(factD),
                new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB }),
                new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factC }),
                new ItemSet<IFact<string>>(new List<IFact<string>>() { factB, factC }),
                new ItemSet<IFact<string>>(new List<IFact<string>>() { factB, factD }),
                new ItemSet<IFact<string>>(new List<IFact<string>>() { factC, factD }),
                new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB, factC }),
                new ItemSet<IFact<string>>(new List<IFact<string>>() { factB, factC, factD })
            };

            //When
            var result = candidateRuleGenerator.GenerateCandidateRules(freqItemSets);

            //Then
            var rule = new AssociationRule<string>(new ItemSet<IFact<string>>(factB), new ItemSet<IFact<string>>(factC));

            Assert.Equal(1, result.FindAll(x => x.Equals(rule)).Count);
        }
    }
}
