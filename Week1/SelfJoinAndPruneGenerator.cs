using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class SelfJoinAndPruneGenerator : ICandidateGenerator<ChessGame>
    {
        public List<ItemSet<IFact<ChessGame>>> GenerateCandidateItemSets(List<ItemSet<IFact<ChessGame>>> frequentItemSets)
        {
            var output = frequentItemSets.SelectMany(x => frequentItemSets, (x, y) => new { l = x.Items, r = y.Items })
                .Where(x => (new ItemSet<IFact<ChessGame>>(x.l.Take(x.l.Count - 1)))
                    .Equals(new ItemSet<IFact<ChessGame>>(x.r.Take(x.r.Count - 1))) && x.l.Last().CompareTo(x.r.Last()) < 0 && !HasImplication(x.l.Last(), x.r.Last()))
               .Select((x) =>
                   {
                       var result = new List<IFact<ChessGame>>(x.l);
                       result.Add(x.r.Last());
                       return result;
                   }
               )
               .Where(x => !IsPruned(frequentItemSets, x))
               .Select(x => new ItemSet<IFact<ChessGame>>(x));

             return output.ToList();
        }

        private bool HasImplication(IFact<ChessGame> fact1, IFact<ChessGame> fact2)
        {
            return fact1.Implies(fact2) || fact2.Implies(fact1);
        }

        private bool IsPruned(List<ItemSet<IFact<ChessGame>>> frequentItemSets, List<IFact<ChessGame>> items)
        {

            for (var i = 0; i < items.Count - 2; i++ )
            {
                var list = new List<IFact<ChessGame>>(items);
                list.RemoveAt(i);                                           //for each subset of genereated itemset

                var subset = new ItemSet<IFact<ChessGame>>(list);

                if (!frequentItemSets.Contains(subset))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
