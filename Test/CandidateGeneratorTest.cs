using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week1.Mocks;
using Xunit;

namespace Week1
{
    public class When_GenerateCandidateItemSets_is_called 
    {
        [Fact]
        public void Expect_non_frequent_candidates_to_be_pruned()
        {
            IFact<string> factA;
            IFact<string> factB;
            IFact<string> factC;
            IFact<string> factD;
            IFact<string> factE;


            //Given
            var candidateGenerator = new SelfJoinAndPruneGenerator<string>();
            factA = new MockFact("A");
            factB = new MockFact("B");
            factC = new MockFact("C");
            factD = new MockFact("D");
            factE = new MockFact("E");

            var abc = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB, factC });
            var abd = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB, factD });
            var acd = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factC, factD });
            var ace = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factC, factE });
            var bcd = new ItemSet<IFact<string>>(new List<IFact<string>>() { factB, factC, factD });

            List<ItemSet<IFact<string>>> frequentThreeItemSets = new List<ItemSet<IFact<string>>>() 
            {
                abc,
                abd,
                acd,
                ace,
                bcd
            };

            //When
            var result = candidateGenerator.GenerateCandidateItemSets(frequentThreeItemSets);

            //Then
            var abcd = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB, factC, factD });

            Assert.Equal(1, result.Count);
            Assert.True(result.Any(itemSet => itemSet.Items.SequenceEqual(abcd.Items))); 
        }

        [Fact]
        public void Expect_correct_set_to_be_generated()
        {
            IFact<string> factA = new MockFact("A");
            IFact<string> factB = new MockFact("B");

            //Given
            var candidateGenerator = new SelfJoinAndPruneGenerator<string>();

            var a = new ItemSet<IFact<string>>(factA);
            var b = new ItemSet<IFact<string>>(factB);

            //When
            var result = candidateGenerator.GenerateCandidateItemSets(new List<ItemSet<IFact<string>>>() {a,b});

            //Then
            Assert.Equal(1, result.Count);
        }
    }
}
