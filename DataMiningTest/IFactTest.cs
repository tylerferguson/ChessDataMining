using DataMining.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataMining
{
    public class IFactTest
    {
        [Fact]
        public void When_CompareTo_is_called_with_two_facts_of_same_type_it_should_give_the_opposite_result_to_the_equivalent_call_on_the_other_fact()
        {
            //Given
            var fact = new MockFact("tf235");
            var otherFact = new MockFact("tailuge");

            Assert.Equal(fact.CompareTo(otherFact), - otherFact.CompareTo(fact));
        }

        [Fact]
        public void When_CompareTo_is_called_with_a_different_fact_it_should_give_the_opposite_result_to_the_equivalent_call_on_the_other_fact()
        {
            //Given
            var fact = new MockFact("tf235");
            var otherFact = new OtherMockFact("King's Gambit");

            Assert.Equal(fact.CompareTo(otherFact), - otherFact.CompareTo(fact));
        }
        
    }
}
