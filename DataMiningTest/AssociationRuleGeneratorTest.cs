using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Mocks;
using Xunit;

namespace DataMining
{
    namespace When_Generate_is_called
    {
        public class With_no_projection_or_target_facts
        {
            IFrequentPatternsMiner<string> apriori;
            ICandidateRuleGenerator<string> candidateRuleGenerator;
            IThresholdFilterer<string> filterer;
            IFact<string> factA;
            IFact<string> factB;
            IFact<string> factC;

            public With_no_projection_or_target_facts()
            {
                var mockFactsGenerators = new List<IFactsGenerator<string>>()
                {
                    new MockFactsGenerator()
                };
                factA = new MockSimpleFact("A");
                factB = new MockSimpleFact("B");
                factC = new MockSimpleFact("C");
                var candidateGenerator = new SelfJoinAndPruneGenerator<string>();
                apriori = new Apriori<string>(candidateGenerator, mockFactsGenerators);
                candidateRuleGenerator = new CandidateRuleGenerator<string>();
                filterer = new ThresholdFilterer<string>();
            }

            [Fact]
            public void If_no_rules_exist_then_an_empty_list_is_returned()
            {
                //Given
                var singleItemSet = new ItemSet<IFact<string>>(factA);

                Database<string> database = new Database<string>(new List<string>() { "A" });

                var ruleGenerator = new AssociationRuleGenerator<string>(database, apriori, candidateRuleGenerator, filterer);

                //When
                var rules = ruleGenerator.Generate(0, 0);

                //Then
                Assert.Empty(rules);
            }

            [Fact]
            public void All_rules_are_returned_for_minsup_0_and_minconf_0()
            {
                //Given
                var a = new ItemSet<IFact<string>>(factA);
                var b = new ItemSet<IFact<string>>(factB);

                Database<string> database = new Database<string>(new List<string>() { "A", "B" });

                var ruleGenerator = new AssociationRuleGenerator<string>(database, apriori, candidateRuleGenerator, filterer);

                //When
                var rules = ruleGenerator.Generate(0, 0);

                //Then
                var aImpliesB = new AssociationRule<string>(a, b);
                var bImpliesA = new AssociationRule<string>(b, a);

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
                var ab = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB });
                var abc = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB, factC });
                var bc = new ItemSet<IFact<string>>(new List<IFact<string>>() { factB, factC });

                Database<string> database = new Database<string>(new List<string>() { "AB", "ABC", "BC" });


                var ruleGenerator = new AssociationRuleGenerator<string>(database, apriori, candidateRuleGenerator, filterer);

                //When
                var rules = ruleGenerator.Generate(0.5, 0.6);

                //Then
                var a = new ItemSet<IFact<string>>(factA);
                var b = new ItemSet<IFact<string>>(factB);
                var c = new ItemSet<IFact<string>>(factC);

                var aImpliesB = new AssociationRule<string>(a, b);
                var bImpliesA = new AssociationRule<string>(b, a);
                var cImpliesB = new AssociationRule<string>(c, b);
                var bImpliesC = new AssociationRule<string>(b, c);

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
                Assert.Equal(2.0 / 3, cImpliesB.RelativeSupport);
                Assert.Equal(2.0 / 3, bImpliesC.RelativeSupport);

                Assert.Equal(1, aImpliesB.Confidence);
                Assert.Equal(2.0 / 3, bImpliesA.Confidence);
                Assert.Equal(1, cImpliesB.Confidence);
                Assert.Equal(2.0 / 3, bImpliesC.Confidence);
            }
        }

        public class With_no_target_facts
        {
            ICandidateRuleGenerator<string> mockRuleGenerator;
            IThresholdFilterer<string> mockFilterer;

            IFact<string> correctFact;
            IFact<string> factA;
            IFact<string> factB;
            IFact<string> factAB;
            IFact<string> factC;

            public With_no_target_facts()
            {
                correctFact = new MockSimpleFact("Correct!");
                factA = new MockSimpleFact("A");
                factB = new MockSimpleFact("B");
                factAB = new MockSimpleFact("AB");
                factC = new MockSimpleFact("C");

                mockRuleGenerator = new MockRuleGenerator();
                mockFilterer = new MockThresholdFilterer();
            }

            [Fact]
            public void And_one_projection_fact_then_rules_are_generated_from_only_the_projected_database()
            {
                //Given
                var mockMiner = new MockMiner();
                mockMiner.Setup(new List<string>() { "A", "AB" }, new List<string>() { "A", "AB" });
                Database<string> database = new Database<string>(new List<string>() { "A", "B", "AB", "BC", "D" });
                var associationRuleGenerator = new AssociationRuleGenerator<string>(database, mockMiner, mockRuleGenerator, mockFilterer);

                //When
                associationRuleGenerator.Generate(0.0, 0.0, new List<IFact<string>>() { factA });

                //Then
                Assert.True(mockMiner.ReceivedCorrectProjectedDatabase);
                Assert.True(mockMiner.ReceivedCorrectTargetDatabase);
            }

            [Fact]
            public void And_two_projection_facts_then_rules_are_generated_from_only_the_projected_database()
            {
                //Given
                var mockMiner = new MockMiner();
                mockMiner.Setup(new List<string>() { "AB" }, new List<string>() { "AB" });
                Database<string> database = new Database<string>(new List<string>() { "A", "B", "AB", "BC", "D" });
                var associationRuleGenerator = new AssociationRuleGenerator<string>(database, mockMiner, mockRuleGenerator, mockFilterer);

                //When
                var result = associationRuleGenerator.Generate(0.0, 0.0, new List<IFact<string>>() { factA, factB });

                //Then
                Assert.True(mockMiner.ReceivedCorrectProjectedDatabase);
                Assert.True(mockMiner.ReceivedCorrectTargetDatabase);
            }
        }

        public class With_no_projection_facts
        {
            ICandidateRuleGenerator<string> mockRuleGenerator;
            IThresholdFilterer<string> mockFilterer;

            IFact<string> correctFact;
            IFact<string> factA;
            IFact<string> factB;
            IFact<string> factAB;
            IFact<string> factC;

            public With_no_projection_facts() 
            {
                correctFact = new MockSimpleFact("Correct!");
                factA = new MockSimpleFact("A");
                factB = new MockSimpleFact("B");
                factAB = new MockSimpleFact("AB");
                factC = new MockSimpleFact("C");

                mockRuleGenerator = new MockRuleGenerator();
                mockFilterer = new MockThresholdFilterer();
            }

            [Fact]
            public void And_at_least_one_target_fact_then_rules_are_generated_from_target_db_with_respect_to_projection_db()
            {
                //Given
                Database<string> database = new Database<string>(new List<string>() { "A", "B", "AB", "BC", "D" });
                var mockMiner = new MockMiner();
                mockMiner.Setup(database.Transactions, new List<string>() { "A", "AB" });
                var associationRuleGenerator = new AssociationRuleGenerator<string>(database, mockMiner, mockRuleGenerator, mockFilterer);

                //When
                var result = associationRuleGenerator.Generate(0.0, 0.0, targetFacts: new List<IFact<string>>() { factA });

                //Then
                Assert.True(mockMiner.ReceivedCorrectProjectedDatabase);
                Assert.True(mockMiner.ReceivedCorrectTargetDatabase);
            }

            [Fact]
            public void And_two_target_facts_then_rules_are_generated_from_target_db_with_respect_to_projection_db()
            {
                //Given
                Database<string> database = new Database<string>(new List<string>() { "A", "B", "AB", "BC", "D" });
                var mockMiner = new MockMiner();
                mockMiner.Setup(database.Transactions, new List<string>() { "AB" });
                var associationRuleGenerator = new AssociationRuleGenerator<string>(database, mockMiner, mockRuleGenerator, mockFilterer);

                //When
                var result = associationRuleGenerator.Generate(0.0, 0.0, targetFacts: new List<IFact<string>>() { factA, factB });

                //Then
                Assert.True(mockMiner.ReceivedCorrectProjectedDatabase);
                Assert.True(mockMiner.ReceivedCorrectTargetDatabase);
            } 
        }

    }
}
