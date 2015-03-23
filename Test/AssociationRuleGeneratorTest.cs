using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Week1
{
    public class When_Generate_is_called
    {
        IFrequentPatternsMiner apriori;
        ICandidateRuleGenerator candidateRuleGenerator;
        IFact factA;
        IFact factB;
        IFact factC;

        public When_Generate_is_called()
        {
            var candidateGenerator = new SelfJoinAndPruneGenerator();
            apriori = new Apriori(candidateGenerator);
            candidateRuleGenerator = new CandidateRuleGenerator();
            factA = new SimpleFact("Name", "A");
            factB = new SimpleFact("Name", "B");
            factC = new SimpleFact("Name", "C");
        }

        [Fact]
        public void If_no_rules_exist_then_an_empty_list_is_returned()
        {
            //Given
            var singleItemSet = new ItemSet(factA);

            List<ItemSet> database = new List<ItemSet>()
            {
                singleItemSet
            };

            var ruleGenerator = new AssociationRuleGenerator(database, apriori, candidateRuleGenerator);

            //When
            var rules = ruleGenerator.Generate(0, 0);

            //Then
            Assert.Empty(rules);
        }

        [Fact]
        public void All_rules_are_returned_for_minsup_0_and_minconf_0()
        {
            //Given
            var a = new ItemSet(factA);
            var b = new ItemSet(factB);

            List<ItemSet> database = new List<ItemSet>()
            {
                a, b
            };

            var ruleGenerator = new AssociationRuleGenerator(database, apriori, candidateRuleGenerator);

            //When
            var rules = ruleGenerator.Generate(0, 0);

            //Then
            var aImpliesB = new AssociationRule(a, b);
            var bImpliesA = new AssociationRule(b, a);

            Assert.Equal(2, rules.Count);
            Assert.Contains(aImpliesB, rules);
            Assert.Contains(bImpliesA, rules);

            aImpliesB = rules.Find(x => x.Equals(aImpliesB));
            bImpliesA = rules.Find(x => x.Equals(bImpliesA));
            Assert.Equal(0, aImpliesB.RelativeSupport);
            Assert.Equal(0, bImpliesA.RelativeSupport);
            Assert.Equal(0, aImpliesB.Confidence);
            Assert.Equal(0, bImpliesA.Confidence);
        }

        [Fact]
        public void Only_rules_meeting_minsup_and_minconf_requirements_are_returned()
        {
            //Given
            var ab = new ItemSet(new List<IFact>() { factA, factB });
            var abc = new ItemSet(new List<IFact>() { factA, factB, factC });
            var bc = new ItemSet(new List<IFact>() { factB, factC });

            List<ItemSet> database = new List<ItemSet>()
            {
                ab, abc, bc
            };

            var ruleGenerator = new AssociationRuleGenerator(database, apriori, candidateRuleGenerator);

            //When
            var rules = ruleGenerator.Generate(0.5, 0.6);

            //Then
            var a = new ItemSet(factA);
            var b = new ItemSet(factB);
            var c = new ItemSet(factC);

            var aImpliesB = new AssociationRule(a, b);
            var bImpliesA = new AssociationRule(b, a);
            var cImpliesB = new AssociationRule(c, b);
            var bImpliesC = new AssociationRule(b, c);

            Assert.Equal(4, rules.Count);

            Assert.Contains(aImpliesB, rules);
            Assert.Contains(bImpliesA, rules);
            Assert.Contains(cImpliesB, rules);
            Assert.Contains(bImpliesC, rules);

            aImpliesB = rules.Find(x => x.Equals(aImpliesB));
            bImpliesA = rules.Find(x => x.Equals(bImpliesA));
            cImpliesB = rules.Find(x => x.Equals(cImpliesB));
            bImpliesC = rules.Find(x => x.Equals(bImpliesC));

            Assert.Equal(2.0 / 3, aImpliesB.RelativeSupport);
            Assert.Equal(2.0 / 3, bImpliesA.RelativeSupport);
            Assert.Equal(2.0/ 3, cImpliesB.RelativeSupport);
            Assert.Equal(2.0 / 3, bImpliesC.RelativeSupport);

            Assert.Equal(1, aImpliesB.Confidence);
            Assert.Equal(2.0 / 3, bImpliesA.Confidence);
            Assert.Equal(1, cImpliesB.Confidence);
            Assert.Equal(2.0 / 3, bImpliesC.Confidence);
        }
    }
}
