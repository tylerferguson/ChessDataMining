using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class OpeningsFactsGenerator : IFactsGenerator<ChessGame>
    {
        public List<IFact<ChessGame>> Generate(List<IFact<ChessGame>> excludedFacts, ChessGame game)
        {
            var index = game.Opening.IndexOf(",");
            var value = index > -1 ? game.Opening.Remove(index) : game.Opening;
            var result = new List<IFact<ChessGame>>() 
            {
                new OpeningFact(game.Opening),
                new OpeningFact(value)
            };

            return result.Where(x => !excludedFacts.Contains(x)).ToList();
        }
    }
}
