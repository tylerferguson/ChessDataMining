using DataMining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChessDataMining
{
    public class When_OpeningFactsGenerator_Generate_Is_Called
    {
        [Fact]
        public void Without_any_excluded_facts_then_all_possible_opening_facts_are_generated()
        {
            //Given
            var openingsFactsGenerator = new OpeningsFactsGenerator();
            var chessGame = new ChessGame();
            chessGame.White ="tailuge";
            chessGame.Black ="sydeman";
            chessGame.Result = "1-0";
            chessGame.Opening = "King's Gambit, Accepted";

            //When
            var result = openingsFactsGenerator.Generate(new List<IFact<ChessGame>>(), chessGame);

            //Then
            var kingsGambitFact = new OpeningFact("King's Gambit");
            var kingsGambitAcceptedFact = new OpeningFact("King's Gambit, Accepted");
            var gambit = new OpeningFact("Gambit");

            Assert.Equal(3, result.Count);
            Assert.Contains(kingsGambitFact, result);
            Assert.Contains(kingsGambitAcceptedFact, result);
            Assert.Contains(gambit, result);
        }

        [Theory]
        [InlineData("King's Pawn Opening")]
        [InlineData("?")]
        public void Without_any_excluded_facts_and_with_an_opening_without_any_variants_then_no_variant_openings_are_generated(string opening)
        {
            //Given
            var openingsFactsGenerator = new OpeningsFactsGenerator();
            var chessGame = new ChessGame();
            chessGame.White = "tailuge";
            chessGame.Black = "sydeman";
            chessGame.Result = "1-0";
            chessGame.Opening = opening;

            //When
            var result = openingsFactsGenerator.Generate(new List<IFact<ChessGame>>(), chessGame);

            //Then
            var openingFact = new OpeningFact(opening);

            Assert.Equal(1, result.Count);
            Assert.Contains(openingFact, result);
        }

        [Theory]
        [InlineData("King's Gambit")]
        [InlineData("Danish Gambit")]
        public void Without_any_excluded_facts_and_with_a_gambit_opening_without_any_variants_then_no_variant_openings_are_generated(string opening)
        {
            //Given
            var openingsFactsGenerator = new OpeningsFactsGenerator();
            var chessGame = new ChessGame();
            chessGame.White = "tailuge";
            chessGame.Black = "sydeman";
            chessGame.Result = "1-0";
            chessGame.Opening = opening;

            //When
            var result = openingsFactsGenerator.Generate(new List<IFact<ChessGame>>(), chessGame);

            //Then
            var openingFact = new OpeningFact(opening);
            var gambit = new OpeningFact("Gambit");

            Assert.Equal(2, result.Count);
            Assert.Contains(openingFact, result);
            Assert.Contains(gambit, result);
        }

        [Fact]
        public void With_excluded_facts_then_opening_facts_are_generated_that_are_not_in_the_exclusion_list()
        {
            //Given
            var openingsFactsGenerator = new OpeningsFactsGenerator();
            var chessGame = new ChessGame();
            chessGame.White = "tailuge";
            chessGame.Black = "sydeman";
            chessGame.Result = "1-0";
            chessGame.Opening = "King's Gambit, Accepted";

            //When
            var kingsGambitAcceptedFact = new OpeningFact("King's Gambit, Accepted");
            var result = openingsFactsGenerator.Generate(new List<IFact<ChessGame>>() { kingsGambitAcceptedFact }, chessGame);

            //Then
            Assert.Equal(0, result.Count);
        }
    }
}
