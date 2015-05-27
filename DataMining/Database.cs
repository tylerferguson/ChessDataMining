using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining
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

        private List<IFact<T>> GenerateFacts(List<IFactsGenerator<T>> factsGenerators, T transaction)
        {
            var generatedFacts = new List<IFact<T>>();

            factsGenerators.ForEach(factGenerator => generatedFacts.AddRange(factGenerator.Generate(givenFacts, transaction)));
            return generatedFacts;
        }

        public List<ItemSet<IFact<T>>> FindFrequentOneItemSets(int databaseCount, List<IFactsGenerator<T>> factsGenerators, Double relativeMinsup)
        {
            Dictionary<IFact<T>, ItemSet<IFact<T>>> candidateItems = new Dictionary<IFact<T>, ItemSet<IFact<T>>>();

            Transactions.ForEach(transaction => GenerateFacts(factsGenerators, transaction).ForEach(fact =>
            {
                //foreach (var key in candidateItems.Keys)
                //{
                //    if (fact.Implies(key))
                //    {
                //        candidateItems[key].AbsoluteSupport++;
                //    }
                //}

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
                itemSet.RelativeSupport = (Double) itemSet.AbsoluteSupport / databaseCount;
                return itemSet.RelativeSupport >= relativeMinsup;
            }).ToList();
        }

        public Database<T> Project(IFact<T> fact)
        {
            var newGivenFacts = new List<IFact<T>>(givenFacts); 
            newGivenFacts.Add(fact);

            return new Database<T>(newGivenFacts, Transactions.Where(x => fact.IsTrue(x)).ToList());
        }

        public Double CalculateSupport(ItemSet<IFact<T>> itemset)
        {
            Transactions.ForEach(transaction => 
            {
                if (itemset.Items.All(fact => fact.IsTrue(transaction))) 
                {
                    itemset.AbsoluteSupport++;
                }
            });

            return (Double) itemset.AbsoluteSupport / Transactions.Count;
        }
    }
}
