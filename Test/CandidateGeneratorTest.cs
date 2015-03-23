using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Week1
{
    public class When_GenerateCandidateItemSets_is_called 
    {
        [Fact]
        public void Expect_non_frequent_candidates_to_be_pruned()
        {
            IFact factA;
            IFact factB;
            IFact factC;
            IFact factD;
            IFact factE;


            //Given
            var candidateGenerator = new SelfJoinAndPruneGenerator();
            factA = new SimpleFact("Name", "A");
            factB = new SimpleFact("Name", "B");
            factC = new SimpleFact("Name", "C");
            factD = new SimpleFact("Name", "D");
            factE = new SimpleFact("Name", "E");

            var abc = new ItemSet(new List<IFact>() { factA, factB, factC });
            var abd = new ItemSet(new List<IFact>() { factA, factB, factD });
            var acd = new ItemSet(new List<IFact>() { factA, factC, factD });
            var ace = new ItemSet(new List<IFact>() { factA, factC, factE });
            var bcd = new ItemSet(new List<IFact>() { factB, factC, factD });

            List<ItemSet> frequentThreeItemSets = new List<ItemSet>() 
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
            var abcd = new ItemSet(new List<IFact>() { factA, factB, factC, factD });

            Assert.Equal(1, result.Count);
            Assert.True(result.Any(itemSet => itemSet.Items.SequenceEqual(abcd.Items))); 
        }

        [Fact]
        public void Expect_correct_set_to_be_generated()
        {
            IFact factA = new SimpleFact("Name", "A");
            IFact factB = new SimpleFact("Name", "B");

            //Given
            var candidateGenerator = new SelfJoinAndPruneGenerator();

            var a = new ItemSet(factA);
            var b = new ItemSet(factB);

            //When
            var result = candidateGenerator.GenerateCandidateItemSets(new List<ItemSet>() {a,b});

            //Then
            Assert.Equal(1, result.Count);
        }
    }
}
