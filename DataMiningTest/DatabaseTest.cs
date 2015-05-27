using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Mocks;
using Xunit;

namespace DataMining
{
    public class When_FindFrequentOneItemSets_is_called
    {
        [Fact]
        public void All_frequent_itemsets_are_returned_with_the_correct_support_accounting_for_itemsets_implying_other_itemsets()
        {
            //Given
            var transactions = new List<string>() {"dog", "cat", "small dog", "big dog"};
            var database = new Database<string>(transactions);
            var mockFactsGenerator = new MockFactsGenerator2();

            var databaseCount = database.Transactions.Count;

            //When
            var result = database.FindFrequentOneItemSets(databaseCount, new List<IFactsGenerator<string>>() { mockFactsGenerator }, 0.3);

            //Then
            var expectedItemSet = new ItemSet<IFact<string>>(new MockOpeningFact("dog"));

            Assert.Equal(1, result.Count);
            Assert.Contains(expectedItemSet, result);
        }
    }
}
