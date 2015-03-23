using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class Database<T>
    {
        public List<T> Transactions { get; set; }
        private List<IFact<T>> givenFacts = new List<IFact<T>>();
 
        public Database(List<T> transactions)
        {
            Transactions = transactions;
        }

        public Database(List<IFact<T>> projectedFact, List<T> transactions) : this(transactions)
        {  
            this.givenFacts.AddRange(projectedFact);
        }

        private List<IFact<T>> GenerateFacts(IFactsFactory<T> factsFactory, T transaction)
        {
            return factsFactory.Generate(givenFacts, transaction);
        }

        public List<ItemSet<IFact<T>>> FindFrequentOneItemSets(int projectedCount, IFactsFactory<T> factsFactory, Double relativeMinsup)
        {
            Dictionary<IFact<T>, ItemSet<IFact<T>>> candidateItems = new Dictionary<IFact<T>, ItemSet<IFact<T>>>();

            Transactions.ForEach(transaction => GenerateFacts(factsFactory, transaction).ForEach(fact =>
            {
                if (!candidateItems.ContainsKey(fact))
                {
                    var itemSet = new ItemSet<IFact<T>>(fact);
                    itemSet.AbsoluteSupport = 1;
                    candidateItems.Add(fact, itemSet);
                }
                else
                {
                    candidateItems[fact].AbsoluteSupport++;
                }
            }));

            return candidateItems.Values.Where(itemSet =>
            {
                itemSet.RelativeSupport = (Double) itemSet.AbsoluteSupport / projectedCount;
                return itemSet.RelativeSupport >= relativeMinsup;
            }).ToList();
        }

        public Database<T> Project(IFact<T> fact)
        {
            var newGivenFacts = new List<IFact<T>>(givenFacts); 
            newGivenFacts.Add(fact);

            return new Database<T>(newGivenFacts, Transactions.Where(x => fact.isTrue(x)).ToList());
        }

        public Double CalculateSupport(ItemSet<IFact<T>> itemset)
        {
            Transactions.ForEach(transaction => 
            {
                if (itemset.Items.All(fact => fact.isTrue(transaction))) 
                {
                    itemset.AbsoluteSupport++;
                }
            });

            return (Double) itemset.AbsoluteSupport / Transactions.Count;
        }
    }
}
