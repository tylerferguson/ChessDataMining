using ChessDataMining.FactGenerators;
using ChessDataMining.Facts;
using DataMining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDataMining
{
    public class ChessDataMiner
    {
        private IEnumerable<ChessGame> games;

        public ChessDataMiner(IEnumerable<ChessGame> games)
        {
            this.games = games;
        }

        public IEnumerable<AssociationRule<ChessGame>> Mine(Double relativeMinsup, Double minconf, IEnumerable<IFact<ChessGame>> projectionFacts = null, IEnumerable<IFact<ChessGame>> targetFacts = null)
        {
            //Fact Generators 
            var openingFactsGenerator = new OpeningsFactsGenerator();
            var simpleFactsGenerator = new SimpleFactsGenerator();
            var timeControlFactsGenerator = new TimeControlFactsGenerator(new TimeControlCategoriser());
            var takesFirstFactGenerator = new TakesFirstFactGenerator();

            var candidateGenerator = new SelfJoinAndPruneGenerator<ChessGame>();
            var factGenerators = new List<IFactsGenerator<ChessGame>>() { simpleFactsGenerator, openingFactsGenerator, timeControlFactsGenerator, takesFirstFactGenerator };
            var apriori = new Apriori<ChessGame>(candidateGenerator, factGenerators);
            var filterer = new ThresholdFilterer<ChessGame>();
            var candidateRuleGenerator = new CandidateRuleGenerator<ChessGame>();

            var database = new Database<ChessGame>(games.ToList());
            var ruleGenerator = new AssociationRuleGenerator<ChessGame>(database, apriori, candidateRuleGenerator, filterer);

            return ruleGenerator.Generate(relativeMinsup, minconf, projectionFacts, targetFacts);
        }
    }
}
