using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class SimpleFactsFactory : IFactsFactory<ChessGame>
    {
        public List<IFact<ChessGame>> Generate(List<IFact<ChessGame>> excludedFacts, ChessGame game)
        {
            var result = new List<IFact<ChessGame>>() 
            {
                new SimpleFact("White", game.White),
                new SimpleFact("Black", game.Black),
                new SimpleFact("Result", game.Result),
                new SimpleFact("Opening", game.Opening)
            };

            return result.Where(x => !excludedFacts.Contains(x)).ToList();
        }
    }
}
