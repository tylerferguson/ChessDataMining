using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class CandidateRuleGenerator : ICandidateRuleGenerator<ChessGame>
    {
        public List<AssociationRule<ChessGame>> GenerateCandidateRules(List<ItemSet<IFact<ChessGame>>> frequentPatterns)
        {
            var candidateRules = new List<AssociationRule<ChessGame>>();
            frequentPatterns.ForEach(x =>
            {
                var powerSet = PowerSetGenerator<ChessGame>.GeneratePowerSet(x);

                candidateRules.AddRange(powerSet.Where(set => !set.IsEmpty() && set.Items.Count < x.Items.Count).Select(set =>
                {
                    return new AssociationRule<ChessGame>(set, x.Not(set));
                }));

            });
            return candidateRules;
        }

        public List<AssociationRule<ChessGame>> GenerateCandidateRules(List<IFact<ChessGame>> targetFacts, List<ItemSet<IFact<ChessGame>>> frequentPatterns)
        {
            if (targetFacts == null)
            {
                return GenerateCandidateRules(frequentPatterns);
            }

            var candidateRules = new List<AssociationRule<ChessGame>>();
            frequentPatterns.ForEach(x => 
                {
                    var powerSet = PowerSetGenerator<ChessGame>.GeneratePowerSet(x);

                    candidateRules.AddRange(powerSet.Where(set => !set.IsEmpty() && !candidateRules.Any(rule => rule.Left.Equals(set))).Select(set => 
                    {
                        var targetItemset = new ItemSet<IFact<ChessGame>>(targetFacts);
                        return new AssociationRule<ChessGame>(set, targetItemset);
                        
                    }));
                    
                });
            return candidateRules;
        }
    }
}
 