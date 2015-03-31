using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week1.Facts;

namespace Week1.FactGenerators
{
    public class TimeControlFactsGenerator : IFactsGenerator<ChessGame>
    {
        ITimeControlCategoriser categoriser;
        public TimeControlFactsGenerator(ITimeControlCategoriser categoriser)
        {
            this.categoriser = categoriser;
        } 

        public List<IFact<ChessGame>> Generate(List<IFact<ChessGame>> excludedFacts, ChessGame transaction)
        {
            var result = new List<IFact<ChessGame>>() 
            {
                new TimeControlFact(categoriser, transaction.TimeControl)
            };

            return result.Where(x => !excludedFacts.Any(y => y.Implies(x))).ToList();
        }
    }
}
