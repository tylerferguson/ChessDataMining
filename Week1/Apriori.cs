using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class Apriori : IFrequentPatternsMiner<ChessGame>
    {
        private ICandidateGenerator<ChessGame> candidateGenerator;
        private List<IFactsGenerator<ChessGame>> factsGenerators;

        public Apriori(ICandidateGenerator<ChessGame> candidateGenerator, List<IFactsGenerator<ChessGame>> factsGenerators)
        {
            this.candidateGenerator = candidateGenerator;
            this.factsGenerators = factsGenerators;
        }

        public List<ItemSet<IFact<ChessGame>>> Mine(Database<ChessGame> database, Double relativeMinsup)
        {
            return Mine(database, database, relativeMinsup);
        }
        public List<ItemSet<IFact<ChessGame>>> Mine(Database<ChessGame> projectedDatabase, Database<ChessGame> targetDatabase, Double relativeMinsup)
        {
            List<ItemSet<IFact<ChessGame>>> result = new List<ItemSet<IFact<ChessGame>>>();
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

        private static List<ItemSet<IFact<ChessGame>>> findFrequentItemSets(int databaseCount, List<ItemSet<IFact<ChessGame>>> candidateItemSets, Database<ChessGame> database, Double relativeMinsup)
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
