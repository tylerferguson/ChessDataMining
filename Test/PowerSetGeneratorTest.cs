using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Week1
{
    public class When_GeneratePowerSet_is_called
    {
        IFact factA;
        IFact factB;
        IFact factC;

        public When_GeneratePowerSet_is_called()
        {
            factA = new SimpleFact("Name", "A");
            factB = new SimpleFact("Name", "B");
            factC = new SimpleFact("Name", "C");

        }
        [Fact]
        public void With_a_singleton_only_the_correct_set_is_returned()
        {
            //Given
            var a = new ItemSet(factA);
            var empty = new ItemSet();

            //When
            var result = PowerSetGenerator.GeneratePowerSet(a);

            //Then
            Assert.Equal(2, result.Count);
            Assert.Contains(a, result);
            Assert.Contains(empty, result);
        }

        [Fact]
        public void With_a_two_item_set_only_the_correct_sets_are_returned()
        {
            //Given
            var ab = new ItemSet(new List<IFact>() { factA, factB });

            //When
            var result = PowerSetGenerator.GeneratePowerSet(ab);

            //Then
            var a = new ItemSet(factA);
            var b = new ItemSet(factB);
            var empty = new ItemSet();


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
            var abc = new ItemSet(new List<IFact>() { factA, factB, factC });

            //When
            var result = PowerSetGenerator.GeneratePowerSet(abc);

            //Then
            var empty = new ItemSet();
            var a = new ItemSet(factA);
            var b = new ItemSet(factB);
            var c = new ItemSet(factC);
            var ab = new ItemSet(new List<IFact>() { factA, factB });
            var bc = new ItemSet(new List<IFact>() { factB, factC });
            var ac = new ItemSet(new List<IFact>() { factA, factC });

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
