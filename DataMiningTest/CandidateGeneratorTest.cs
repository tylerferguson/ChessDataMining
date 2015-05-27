using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Mocks;
using Xunit;

namespace DataMining
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
            factA = new MockSimpleFact("A");
            factB = new MockSimpleFact("B");
            factC = new MockSimpleFact("C");
            factD = new MockSimpleFact("D");
            factE = new MockSimpleFact("E");

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
            IFact<string> factA = new MockSimpleFact("A");
            IFact<string> factB = new MockSimpleFact("B");

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
