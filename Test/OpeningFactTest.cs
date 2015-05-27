using ChessDataMining.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChessDataMining
{
    public class OpeningFactTest
    {
        [Fact]
        public void When_implies_is_called_without_gambit_opening_and_an_implication_exists()
        {
            //Given
            var thisFact = new OpeningFact("King's Gambit, Accepted");
            var impliedFact = new OpeningFact("King's Gambit");

            //When
            Assert.True(thisFact.Implies(impliedFact)); // <-- Then
        }

        [Fact]
        public void When_implies_is_called_with_gambit_opening_and_an_implication_exists()
        {
            //Given
            var thisFact = new OpeningFact("King's Gambit");
            var impliedFact = new OpeningFact("Gambit");

            //When
            Assert.True(thisFact.Implies(impliedFact)); // <-- Then
        }

        [Fact]
        public void When_implies_is_called_with_openings_without_any_words_in_common_and_no_implication_exists()
        {
            //Given
            var thisFact = new OpeningFact("King's Gambit");
            var impliedFact = new OpeningFact("Indian Game");

            //When
            Assert.False(thisFact.Implies(impliedFact)); // <-- Then
        }

        [Fact]
        public void When_implies_is_called_with_openings_with_words_in_common_and_no_implication_exists()
        {
            //Given
            var thisFact = new OpeningFact("King's Gambit, Modern Defense");
            var impliedFact = new OpeningFact("Modern Defense");

            //When
            Assert.False(thisFact.Implies(impliedFact)); // <-- Then
            Assert.False(impliedFact.Implies(thisFact));
        }

        [Fact]
        public void When_IsTrue_is_called_with_matching_opening_return_true()
        {
            //Given
            var thisFact = new OpeningFact("King's Gambit, Accepted");
            var game = new ChessGame();
            game.Opening = "King's Gambit, Accepted";

            //When
            Assert.True(thisFact.IsTrue(game)); // <-- Then
        }

        [Fact]
        public void When_IsTrue_is_called_with_satisying_opening_return_true()
        {
            //Given
            var thisFact = new OpeningFact("King's Gambit");
            var game = new ChessGame();
            game.Opening = "King's Gambit, Accepted";

            //When
            Assert.True(thisFact.IsTrue(game)); // <-- Then
        }

        [Fact]
        public void When_IsTrue_is_called_on_GambitOpeningFact_with_satisying_opening_return_true()
        {
            //Given
            var thisFact = new OpeningFact("Gambit");
            var game = new ChessGame();
            game.Opening = "King's Gambit, Accepted";

            //When
            Assert.True(thisFact.IsTrue(game)); // <-- Then
        }

        [Fact]
        public void When_IsTrue_is_called_without_satisying_opening_return_false()
        {
            //Given
            var thisFact = new OpeningFact("Indian Game");
            var game = new ChessGame();
            game.Opening = "King's Gambit, Accepted";

            //When
            Assert.False(thisFact.IsTrue(game)); // <-- Then
        }

        [Fact]
        public void When_IsTrue_is_called_with_non_satisying_opening_with_words_in_common_return_false()
        {
            //Given
            var thisFact = new OpeningFact("King's Gambit, Accepted, Modern Defense");
            var game = new ChessGame();
            game.Opening = "Modern Defense";

            //When
            Assert.False(thisFact.IsTrue(game)); // <-- Then

            //Given
            thisFact = new OpeningFact("Modern Defense");
            game.Opening = "King's Gambit, Accepted, Modern Defense";

            //When
            Assert.False(thisFact.IsTrue(game)); // <-- Then
        }
    }
}
