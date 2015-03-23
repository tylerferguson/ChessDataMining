using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Week1
{
    public class When_GenerateCandidateRules_is_called
    {
        [Fact]
        public void No_duplicate_candidate_rules_should_be_returned_for_maximal_freq_itemsets_that_overlap()
        {
            //Given
            IFact factA;
            IFact factB;
            IFact factC;
            IFact factD;

            factA = new SimpleFact("Name", "A");
            factB = new SimpleFact("Name", "B");
            factC = new SimpleFact("Name", "C");
            factD = new SimpleFact("Name", "D");

            var candidateRuleGenerator = new CandidateRuleGenerator();

            var freqItemSets = new List<ItemSet>() 
            {
                new ItemSet(factA),
                new ItemSet(factB),
                new ItemSet(factC),
                new ItemSet(factD),
                new ItemSet(new List<IFact>() { factA, factB }),
                new ItemSet(new List<IFact>() { factA, factC }),
                new ItemSet(new List<IFact>() { factB, factC }),
                new ItemSet(new List<IFact>() { factB, factD }),
                new ItemSet(new List<IFact>() { factC, factD }),
                new ItemSet(new List<IFact>() { factA, factB, factC }),
                new ItemSet(new List<IFact>() { factB, factC, factD })
            };

            //When
            var result = candidateRuleGenerator.GenerateCandidateRules(freqItemSets);

            //Then
            var rule = new AssociationRule(new ItemSet(factB), new ItemSet(factC));

            Assert.Equal(1, result.FindAll(x => x.Equals(rule)).Count);
        }
    }
}
