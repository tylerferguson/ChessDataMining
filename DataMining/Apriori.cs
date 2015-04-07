using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining
{
    public class Apriori<T> : IFrequentPatternsMiner<T>
    {
        private ICandidateGenerator<T> candidateGenerator;
        private List<IFactsGenerator<T>> factsGenerators;

        public Apriori(ICandidateGenerator<T> candidateGenerator, List<IFactsGenerator<T>> factsGenerators)
        {
            this.candidateGenerator = candidateGenerator;
            this.factsGenerators = factsGenerators;
        }

        public List<ItemSet<IFact<T>>> Mine(Database<T> database, Double relativeMinsup)
        {
            return Mine(database, database, relativeMinsup);
        }

        public List<ItemSet<IFact<T>>> Mine(Database<T> projectedDatabase, Database<T> targetDatabase, Double relativeMinsup)
        {
            List<ItemSet<IFact<T>>> result = new List<ItemSet<IFact<T>>>();
            var projectedCount = projectedDatabase.Transactions.Count;

            var frequentItemSets = targetDatabase.FindFrequentOneItemSets(projectedCount, factsGenerators, relativeMinsup);
           
            
            while (frequentItemSets.Any())
            {
                result.AddRange(frequentItemSets);

                var candidateItemSets = candidateGenerator.GenerateCandidateItemSets(frequentItemSets);

                frequentItemSets = findFrequentItemSets(projectedCount, candidateItemSets, targetDatabase, relativeMinsup);
            }

            return result;
        }

        private static List<ItemSet<IFact<T>>> findFrequentItemSets(int databaseCount, List<ItemSet<IFact<T>>> candidateItemSets, Database<T> database, Double relativeMinsup)
        {
            database.Transactions.ForEach(transaction => candidateItemSets.ForEach(candidateItemSet =>
            {
                if (candidateItemSet.Items.All(fact => fact.IsTrue(transaction)))
                {
                    candidateItemSet.AbsoluteSupport++;
                }
            }));

            return candidateItemSets.Where(itemSet =>
            {
                itemSet.RelativeSupport = (Double)itemSet.AbsoluteSupport / databaseCount;
                return itemSet.RelativeSupport >= relativeMinsup;
            }).ToList();
        }

    }
}
