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
    [RoutePrefix("api/AssociationRules")]
    public class AssociationRulesController : ApiController
    {
        public AssociationRulesController() {}

        //POST api/AssociationRules/Mine
        [Route("Mine")]
        public IEnumerable<AssociationRuleDTO> Mine(List<ChessGame> games)
        {
            var projectedFact1 = new OpeningFact("Gambit");
            //var projectedFact2 = new SimpleFact("White", "tailuge");
            var projectedFacts = new List<IFact<ChessGame>>() { projectedFact1 };

            var targetFact = new SimpleFact("Result", "1-0");
            var targetFacts = new List<IFact<ChessGame>>() { targetFact };

            var minsup = 0.01;
            var minconf = 0.1;

            var chessDataMiner = new ChessDataMiner(games);
            var rules = chessDataMiner.Mine(minsup, minconf, projectedFacts, targetFacts);

            return rules.Select(x =>
            {
                return new AssociationRuleDTO()
                {
                    Value = x.ToString(),
                    AbsoluteSupport = x.AbsoluteSupport,
                    Confidence = x.Confidence,
                    LiftCorrelation = x.LiftCorrelation,
                    RelativeSupport = x.RelativeSupport
                };
            });
        }

        // POST api/AssociationRules/Mine
        //[Route("Mine")]
        //public string Mine(Byte[] games)
        //{
        //    return String.Empty;
        //}
    }
}
