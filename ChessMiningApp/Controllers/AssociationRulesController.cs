using ChessDataMining;
using ChessDataMining.FactGenerators;
using ChessDataMining.Facts;
using ChessMiningApp.Models;
using DataMining;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace ChessMiningApp.Controllers
{
    [RoutePrefix("api/AssociationRules")]
    public class AssociationRulesController : ApiController
    {
        public AssociationRulesController() {}

        //GET api/AssociationRules
        public IEnumerable<dynamic> GetFacts()
        {
            var type = typeof(IFact<ChessGame>);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Select(t =>
                    {
                        var enumType = t.GetNestedType("validParam");
                        if (enumType != null)
                        {
                            return new { FactType = t.Name, ValidParams = Enum.GetNames(enumType) };
                        }
                        return new { FactType = t.Name, ValidParams = new string[0] };
                    });
        } 

        //POST api/AssociationRules/Mine
        [Route("Mine")]
        public IEnumerable<AssociationRuleDTO> Mine(MiningDTO dto)
        {
            var projectionFacts = ParseFactDtoCollection(dto.projectionFacts);
            var targetFacts = ParseFactDtoCollection(dto.targetFacts);

            var chessDataMiner = new ChessDataMiner(dto.Games);
            var rules = chessDataMiner.Mine(dto.Minsup, dto.Minconf, projectionFacts, targetFacts);

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

        private IEnumerable<IFact<ChessGame>> ParseFactDtoCollection(IEnumerable<FactDTO> factDtos)
        {
            if (factDtos == null)
            {
               return null;
            }

            return factDtos.Select(factDto => factDto.Parse());
        }
    }
}
