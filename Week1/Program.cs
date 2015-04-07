using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ChessDataMining.FactGenerators;
using DataMining;

namespace ChessDataMining
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ChessGame> result;
            var serializer = new JsonSerializer();
            using (var re = File.OpenText("rated.json"))
            using (var reader = new JsonTextReader(re))
            {
                result = serializer.Deserialize<List<ChessGame>>(reader);
            }

            var database = new Database<ChessGame>(result);

            var projectedFact1 = new OpeningFact("Gambit");
            //var projectedFact2 = new SimpleFact("White", "tailuge");
            var projectedFacts = new List<IFact<ChessGame>>() { projectedFact1 };

            var targetFact = new SimpleFact("Result", "1-0");
            var targetFacts = new List<IFact<ChessGame>>() { targetFact };

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
            var ruleGenerator = new AssociationRuleGenerator<ChessGame>(database, apriori, candidateRuleGenerator, filterer);

            //When
            var minsup = 0.01;
            var minconf = 0.1;
            var rules = ruleGenerator.Generate(minsup, minconf, projectedFacts, targetFacts);

            var i = 1;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\tferguson\Documents\Visual Studio 2013\Projects\PatternDiscoveryInDataMining\Week1\chessAssociationRules.txt"))
            {
                file.WriteLine("Minsup: " + minsup + ", " + "Minconf: " + minconf);
                file.WriteLine("For games where " + projectedFacts[0] + ", ");
                file.WriteLine("there are " + rules.Count + " strong association rules \n");

                foreach (var rule in rules)
                {
                    file.Write(i + ". ");
                    file.WriteLine(rule);
                    i++;
                }
            }

            System.Console.WriteLine("Done!");
            System.Console.ReadLine();

        }
    }
}
