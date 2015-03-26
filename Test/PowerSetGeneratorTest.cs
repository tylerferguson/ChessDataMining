using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week1.Mocks;
using Xunit;

namespace Week1
{
    public class When_GeneratePowerSet_is_called
    {
        IFact<string> factA;
        IFact<string> factB;
        IFact<string> factC;

        public When_GeneratePowerSet_is_called()
        {
            factA = new MockFact("A");
            factB = new MockFact("B");
            factC = new MockFact("C");

        }
        [Fact]
        public void With_a_singleton_only_the_correct_set_is_returned()
        {
            //Given
            var a = new ItemSet<IFact<string>>(factA);
            var empty = new ItemSet<IFact<string>>();

            //When
            var result = PowerSetGenerator<string>.GeneratePowerSet(a);

            //Then
            Assert.Equal(2, result.Count);
            Assert.Contains(a, result);
            Assert.Contains(empty, result);
        }

        [Fact]
        public void With_a_two_item_set_only_the_correct_sets_are_returned()
        {
            //Given
            var ab = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB });

            //When
            var result = PowerSetGenerator<string>.GeneratePowerSet(ab);

            //Then
            var a = new ItemSet<IFact<string>>(factA);
            var b = new ItemSet<IFact<string>>(factB);
            var empty = new ItemSet<IFact<string>>();


            Assert.Equal(4, result.Count);
            Assert.Contains(a, result);
            Assert.Contains(b, result);
            Assert.Contains(ab, result);
            Assert.Contains(empty, result);
        }

        [Fact]
        public void With_a_three_item_set_only_the_correct_sets_are_returned()
        {
            //Given
            var abc = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB, factC });

            //When
            var result = PowerSetGenerator<string>.GeneratePowerSet(abc);

            //Then
            var empty = new ItemSet<IFact<string>>();
            var a = new ItemSet<IFact<string>>(factA);
            var b = new ItemSet<IFact<string>>(factB);
            var c = new ItemSet<IFact<string>>(factC);
            var ab = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factB });
            var bc = new ItemSet<IFact<string>>(new List<IFact<string>>() { factB, factC });
            var ac = new ItemSet<IFact<string>>(new List<IFact<string>>() { factA, factC });

            Assert.Equal(8, result.Count);
            Assert.Contains(a, result);
            Assert.Contains(b, result);
            Assert.Contains(c, result);
            Assert.Contains(ab, result);
            Assert.Contains(ac, result);
            Assert.Contains(bc, result);
            Assert.Contains(abc, result);
            Assert.Contains(empty, result);
        } 
    }
}
