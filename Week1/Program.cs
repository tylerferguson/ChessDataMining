using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Week1
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

            var projectedFacts = new SimpleFact("White", "tailuge");
            var targetFacts = new SimpleFact("Result", "1-0");

            var simpleFactFactory = new SimpleFactsFactory();
            var candidateGenerator = new SelfJoinAndPruneGenerator();
            var apriori = new Apriori(candidateGenerator, simpleFactFactory);
            var candidateRuleGenerator = new CandidateRuleGenerator();
            var ruleGenerator = new AssociationRuleGenerator<ChessGame>(database, apriori, candidateRuleGenerator);

            //When
            var minsup = 0.005;
            var minconf = 0.5;
            var rules = ruleGenerator.Generate(minsup, minconf, new List<IFact<ChessGame>>() { projectedFacts }, new List<IFact<ChessGame>>() { targetFacts });

            var i = 1;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\tferguson\Documents\Visual Studio 2013\Projects\PatternDiscoveryInDataMining\Week1\chessAssociationRules.txt"))
            {
                file.WriteLine("Minsup: " + minsup + ", " + "Minconf: " + minconf);
                file.WriteLine("For games where " + projectedFacts + ", ");
                file.WriteLine("there are " + rules.Count + " strong association rules \n");

                foreach (var rule in rules)
                {
                    file.WriteLine(i + ".");
                    file.WriteLine(rule);
                    i++;
                }
            }

            System.Console.WriteLine("Done!");
            System.Console.ReadLine();

        }
    }
}
