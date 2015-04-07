using ChessDataMining;
using ChessDataMining.FactGenerators;
using ChessMiningApp.Models;
using DataMining;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChessMiningApp.Controllers
{
    public class AssociationRulesController : ApiController
    {
        AssociationRuleGenerator<ChessGame> ruleGenerator;
        List<IFact<ChessGame>> projectedFacts;
        List<IFact<ChessGame>> targetFacts;

        public AssociationRulesController()
        {
            List<ChessGame> result;
            var serializer = new JsonSerializer();
            using (var re = File.OpenText("C:/tferguson/Documents/Visual Studio 2013/Projects/PatternDiscoveryInDataMining/Week1/Week1/rated.json"))
            using (var reader = new JsonTextReader(re))
            {
                result = serializer.Deserialize<List<ChessGame>>(reader);
            }

            var database = new Database<ChessGame>(result);

            var projectedFact1 = new OpeningFact("Gambit");
            //var projectedFact2 = new SimpleFact("White", "tailuge");
            projectedFacts = new List<IFact<ChessGame>>() { projectedFact1 };

            var targetFact = new SimpleFact("Result", "1-0");
            targetFacts = new List<IFact<ChessGame>>() { targetFact };

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
            ruleGenerator = new AssociationRuleGenerator<ChessGame>(database, apriori, candidateRuleGenerator, filterer);
        }

        public IEnumerable<AssociationRule> GetAllRules()
        {
            var minsup = 0.01;
            var minconf = 0.1;
            var result = ruleGenerator.Generate(minsup, minconf, projectedFacts, targetFacts);

            var rules = new List<AssociationRule>();

            result.ForEach(x =>
            {
                var rule = new AssociationRule();
                rule.AbsoluteSupport = x.AbsoluteSupport;
                rule.Confidence = x.Confidence;
                rule.Left = x.Left.ToString();
                rule.LiftCorrelation = x.LiftCorrelation;
                rule.RelativeSupport = x.RelativeSupport;
                rule.Right = x.Right.ToString();

                rules.Add(rule);
            });

            return rules;
        }

    }
}
