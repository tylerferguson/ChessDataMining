using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining
{
    public static class PowerSetGenerator<T>
    {
        public static List<ItemSet<IFact<T>>> GeneratePowerSet(ItemSet<IFact<T>> set)
        {
            if (set.IsEmpty())
            {
                return new List<ItemSet<IFact<T>>>() { new ItemSet<IFact<T>>() };
            }

            var head = new ItemSet<IFact<T>>(set.Items.First());
            var tail = set.Not(head);

            var generatedFromRemoved = GeneratePowerSet(tail);

            return generatedFromRemoved.Select(x => x.Union(head)).Concat(generatedFromRemoved).ToList();
            
        }
    }
}
