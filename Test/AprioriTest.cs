using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Week1
{
    public class When_mine_is_called_the_apriori_algorithm 
    {
        Apriori apriori;
        IFact factA;
        IFact factB;
        IFact factC;

        public When_mine_is_called_the_apriori_algorithm()
        {
            apriori = new Apriori(new SelfJoinAndPruneGenerator());
            factA = new SimpleFact("Name", "A");
            factB = new SimpleFact("Name", "B");
            factC = new SimpleFact("Name", "C");
        }

        [Fact]
        public void Should_return_empty_list_for_an_empty_transaction_database()
        {
            //Given
            List<ItemSet> database = new List<ItemSet>();

            //Then
            Assert.Empty(apriori.mine(database, 1)); //<-- When
        }

        [Fact]
        public void Should_return_the_correct_1_itemset_for_a_single_transaction_single_item_database_and_minsup_1()
        {
            //Given
            var singleItemSet = new ItemSet(factA);

            List<ItemSet> database = new List<ItemSet>()
            {
                singleItemSet
            };


            //When
            var result = apriori.mine(database, 1);

            //Then
            Assert.Equal(1, result.Count);
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 1 && singleItemSet.Equals(itemSet)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0.5)]
        [InlineData(0.25)]
        public void Should_return_the_correct_1_itemset_for_a_multiple_transaction_single_item_database_and_small_enough_minsup(Double minsup)
        {
            //Given
            var singleItemSet = new ItemSet(factA);

            List<ItemSet> database = new List<ItemSet>()
            {
                singleItemSet,
                singleItemSet,
                singleItemSet
            };

            //When
            var result = apriori.mine(database, minsup);

            //Then
            Assert.Equal(1, result.Count);
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 3 && singleItemSet.Equals(itemSet)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0.7)]
        [InlineData(0.6)]
        public void Should_return_empty_list_if_there_are_no_frequent_1_itemsets(Double minsup)
        {
            //Given
            var singleItemSet = new ItemSet(factA);
            var singleItemSetB = new ItemSet(factB);


            List<ItemSet> database = new List<ItemSet>()
            {
                singleItemSet,
                singleItemSetB
            };

            //Then
            Assert.Empty(apriori.mine(database, minsup));
        }

        [Theory]
        [InlineData(0.1)]
        [InlineData(0.05)]

        public void Should_return_multiple_1_itemsets_for_database_with_several_frequent_items_and_small_enough_minsup(Double minsup)
        {
            //Given
            var singleItemSetA = new ItemSet(factA);
            var singleItemSetB= new ItemSet(factB);
            var singleItemSetC = new ItemSet(factC);

            List<ItemSet> database = new List<ItemSet>()
            {
                singleItemSetA,
                singleItemSetB,
                singleItemSetB,
                singleItemSetC,   
                singleItemSetA,
                singleItemSetB,
                singleItemSetC,
            };

            //When
            var result = apriori.mine(database, minsup);

            //Then
            Assert.Equal(3, result.Count);
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 2 && itemSet.Equals(singleItemSetA)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 3 && itemSet.Equals(singleItemSetB)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 2 && itemSet.Equals(singleItemSetC)));

        }

        [Fact]
        public void Should_return_only_the_frequent_1_itemsets_for_databases_with_multiple_transactions()
        {
            //Given
            var singleItemSetA = new ItemSet(factA);
            var singleItemSetB = new ItemSet(factB);
            var singleItemSetC = new ItemSet(factC);

            List<ItemSet> database = new List<ItemSet>()
            {
                singleItemSetA,
                singleItemSetB,
                singleItemSetB,
                singleItemSetC,   
                singleItemSetA,
                singleItemSetB,
            };

            //When
            var result1 = apriori.mine(database, 0.3);
            var result2 = apriori.mine(database, 0.5);

            //Then
            Assert.Equal(2, result1.Count);
            Assert.True(result1.Any(itemSet => itemSet.AbsoluteSupport == 2 && itemSet.Equals(singleItemSetA)));
            Assert.True(result1.Any(itemSet => itemSet.AbsoluteSupport == 3 && itemSet.Equals(singleItemSetB)));


            Assert.Equal(1, result2.Count);
            Assert.True(result1.Any(itemSet => itemSet.AbsoluteSupport == 3 && itemSet.Equals(singleItemSetB)));
        }

        [Fact]
        public void Should_return_correct_2_itemset_and_one_itemsets_for_database_with_a_single_multi_item_transaction_and_minsup_1()
        {
            //Given
            var twoItemSet = new ItemSet(new List<IFact>(){factA, factB});

            List<ItemSet> database = new List<ItemSet>() 
            {
                twoItemSet
            };

            //When
            List<ItemSet> result = apriori.mine(database, 1);

            //Then  
            var singleItemSetA = new ItemSet(factA);
            var singleItemSetB = new ItemSet(factB);

            Assert.Equal(3, result.Count());
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 1 && itemSet.Equals(singleItemSetA)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 1 && itemSet.Equals(singleItemSetB)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 1 && itemSet.Equals(twoItemSet)));
        }

        [Fact]
        public void Should_return_correct_2_itemsets_and_one_itemsets_for_database_with_multiple_multi_item_transactions_and_small_enough_minsup()
        {
            //Given
            var oneItemSetA = new ItemSet(factA);
            var oneItemSetB = new ItemSet(factB);
            var twoItemSetAB = new ItemSet(new List<IFact>() { factA, factB });
            var twoItemSetAB2 = new ItemSet(new List<IFact>() { factA, factB});
            var twoItemSetAC = new ItemSet(new List<IFact>() {factA, factC });     

            List<ItemSet> database = new List<ItemSet>() 
            {
                oneItemSetA,
                oneItemSetB,
                twoItemSetAB,
                twoItemSetAB2,
                twoItemSetAC
            };

            //When
            List<ItemSet> result = apriori.mine(database, 0.2);
            
            //Then
            var oneItemSetC = new ItemSet(new List<IFact>() { factC});

            Assert.Equal(5, result.Count);
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 4 && itemSet.Equals(oneItemSetA)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 3 && itemSet.Equals(oneItemSetB)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 1 && itemSet.Equals(oneItemSetC)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 2 && itemSet.Equals(twoItemSetAB)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 1 && itemSet.Equals(twoItemSetAC)));

            //When
            List<ItemSet> result2 = apriori.mine(database, 0.4);

            //Then
            Assert.Equal(3, result2.Count);
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 4 && itemSet.Equals(oneItemSetA)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 3 && itemSet.Equals(oneItemSetB)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 2 && itemSet.Equals(twoItemSetAB)));

        }

        [Fact]
        public void Should_return_only_frequent_1_itemsets_if_there_are_no_frequent_2_itemsets()
        {
            //Given
            var oneItemSetA = new ItemSet(factA);
            var oneItemSetB = new ItemSet(factB);
            var twoItemSetAB = new ItemSet(new List<IFact>() { factA, factB});
            var twoItemSetAB2 = new ItemSet(new List<IFact>() { factA, factB});
            var twoItemSetAC = new ItemSet(new List<IFact>() { factA, factC });

            List<ItemSet> database = new List<ItemSet>() 
            {
                oneItemSetA,
                oneItemSetB,
                twoItemSetAB,
                twoItemSetAB2,
                twoItemSetAC
            };

            //When
            List<ItemSet> result = apriori.mine(database, 0.6);

            //Then
            Assert.Equal(2, result.Count);
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 4 && itemSet.Equals(oneItemSetA)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 3 && itemSet.Equals(oneItemSetB)));
        }

        [Fact]
        public void Should_return_frequent_3_and_2_and_1_itemsets_for_small_enough_minsup()
        {
            //Given
            var oneItemSetA = new ItemSet(factA);
            var oneItemSetB = new ItemSet(factB);
            var twoItemSetAB = new ItemSet(new List<IFact>() { factA, factB});
            var threeItemSetABC = new ItemSet(new List<IFact>() { factA, factB, factC });
            var twoItemSetAC = new ItemSet(new List<IFact>() { factA, factC });

            List<ItemSet> database = new List<ItemSet>() 
            {
                oneItemSetA,
                oneItemSetB,
                twoItemSetAB,
                threeItemSetABC,
                twoItemSetAC
            };

            //When
            List<ItemSet> result = apriori.mine(database, 0.2);

            //Then
            var oneItemSetC = new ItemSet(factC);
            var twoItemSetBC = new ItemSet(new List<IFact>() { factB, factC});

            Assert.Equal(7, result.Count);
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 4 && itemSet.Equals(oneItemSetA)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 3 && itemSet.Equals(oneItemSetB)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 2 && itemSet.Equals(oneItemSetC)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 2 && itemSet.Equals(twoItemSetAB)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 2 && itemSet.Equals(twoItemSetAC)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 1 && itemSet.Equals(twoItemSetBC)));
            Assert.True(result.Any(itemSet => itemSet.AbsoluteSupport == 1 && itemSet.Equals(threeItemSetABC)));
        }
    }
}
