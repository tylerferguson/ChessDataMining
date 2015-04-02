using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class SelfJoinAndPruneGenerator<T> : ICandidateGenerator<T>
    {
        public List<ItemSet<IFact<T>>> GenerateCandidateItemSets(List<ItemSet<IFact<T>>> frequentItemSets)
        {
            var output = frequentItemSets.SelectMany(x => frequentItemSets, (x, y) => new { l = x.Items, r = y.Items })
                .Where(x => (new ItemSet<IFact<T>>(x.l.Take(x.l.Count - 1))).Equals(new ItemSet<IFact<T>>(x.r.Take(x.r.Count - 1))) 
                    && x.l.Last().CompareTo(x.r.Last()) < 0 
                    && !HasImplication(x.l.Last(), x.r.Last()))
               .Select((x) =>
                   {
                       var result = new List<IFact<T>>(x.l);
                       result.Add(x.r.Last());
                       return result;
                   }
               )
               .Where(x => !IsPruned(frequentItemSets, x))
               .Select(x => new ItemSet<IFact<T>>(x));

             return output.ToList();
        }

        private bool HasImplication(IFact<T> fact1, IFact<T> fact2)
        {
            return fact1.Implies(fact2) || fact2.Implies(fact1);
        }

        private bool IsPruned(List<ItemSet<IFact<T>>> frequentItemSets, List<IFact<T>> items)
        {

            for (var i = 0; i < items.Count - 2; i++ )
            {
                var list = new List<IFact<T>>(items);
                list.RemoveAt(i);                                           //for each subset of genereated itemset

                var subset = new ItemSet<IFact<T>>(list);

                if (!frequentItemSets.Contains(subset))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
