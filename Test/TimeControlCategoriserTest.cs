using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessDataMining.Facts;
using Xunit;

namespace ChessDataMining
{
    public class When_TimeControlCategoriser_Categorise_is_called
    {
        ITimeControlCategoriser categoriser;
        public When_TimeControlCategoriser_Categorise_is_called()
        {
            categoriser = new TimeControlCategoriser();
        }

        [Fact]
        public void With_a_string_without_a_plus_sign_an_exception_is_thrown()
        {
            Assert.ThrowsAny<Exception>(() => categoriser.Categorise("thisstring"));
        }

        [Fact]
        public void With_a_negative_time_an_argument_exception_is_thrown()
        {
            Assert.Throws<ArgumentException>(() => categoriser.Categorise("-1+0"));
            Assert.Throws<ArgumentException>(() => categoriser.Categorise("1+-1"));
            Assert.Throws<ArgumentException>(() => categoriser.Categorise("-1+-1"));
        }

        [Theory]
        [InlineData("60+0", "Bullet")]
        [InlineData("60+3", "Bullet")]
        [InlineData("60+13", "Blitz")]
        [InlineData("60+14", "Classic")]

        [InlineData("120+0", "Bullet")]
        [InlineData("120+1", "Bullet")]
        [InlineData("120+2", "Blitz")]
        [InlineData("120+11", "Blitz")]
        [InlineData("120+12", "Classic")]

        [InlineData("180+0", "Blitz")]
        [InlineData("180+9", "Blitz")]
        [InlineData("180+10", "Classic")]

        [InlineData("240+0", "Blitz")]
        [InlineData("240+7", "Blitz")]
        [InlineData("240+8", "Classic")]

        [InlineData("300+0", "Blitz")]
        [InlineData("300+5", "Blitz")]
        [InlineData("300+6", "Classic")]

        [InlineData("360+0", "Blitz")]
        [InlineData("360+3", "Blitz")]
        [InlineData("360+4", "Classic")]

        [InlineData("420+0", "Blitz")]
        [InlineData("420+1", "Blitz")]
        [InlineData("420+2", "Classic")]

        [InlineData("600+10", "Classic")]
        public void It_correctly_categorises_the_time(string timeControl, string value)
        {
            Assert.Equal(value, categoriser.Categorise(timeControl));
        }
    }
}
