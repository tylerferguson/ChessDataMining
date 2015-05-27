using ChessDataMining.Facts;
using DataMining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDataMining
{
    public class OpeningsFactsGenerator : IFactsGenerator<ChessGame>
    {
        public List<IFact<ChessGame>> Generate(List<IFact<ChessGame>> excludedFacts, ChessGame game)
        {
            var result = GenerateAllOpenings(game.Opening).ToList();

            if (game.Opening.ToLower().Contains("gambit"))
            {
                result.Add(new OpeningFact("Gambit"));
            }
            return result.Where(x => !excludedFacts.Any(y => y.Implies(x))).ToList();
        }

        private IEnumerable<IFact<ChessGame>> GenerateAllOpenings(string opening)
        {
            var result = new List<IFact<ChessGame>>();
            result.Add(new OpeningFact(opening));

            var index = opening.LastIndexOf(",");
            
            if (index < 0)
            {
                return result;
            }
            else
            {
                var parentVariation = opening.Remove(index);
                return result.Concat(GenerateAllOpenings(parentVariation));
            }
        }
    }
}
