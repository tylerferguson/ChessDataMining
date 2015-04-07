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
            var index = game.Opening.IndexOf(",");
            var value = index > -1 ? game.Opening.Remove(index) : game.Opening;
            var result = new List<IFact<ChessGame>>() 
            {
                new OpeningFact(game.Opening)
            };

            if (value != game.Opening)
            {
                result.Add(new OpeningFact(value));
            }
            if (game.Opening.ToLower().Contains("gambit"))
            {
                result.Add(new OpeningFact("Gambit"));
            }

            return result.Where(x => !excludedFacts.Any(y => y.Implies(x))).ToList();
        }
    }
}
