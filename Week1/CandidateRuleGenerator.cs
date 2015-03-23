using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class CandidateRuleGenerator : ICandidateRuleGenerator<ChessGame>
    {

        public List<AssociationRule<ChessGame>> GenerateCandidateRules(List<IFact<ChessGame>> targetFacts, List<ItemSet<IFact<ChessGame>>> frequentPatterns)
        {
            var targetItemset = new ItemSet<IFact<ChessGame>>(targetFacts);
            var candidateRules = new List<AssociationRule<ChessGame>>();
            frequentPatterns.ForEach(x => 
                {
                    var powerSet = PowerSetGenerator<ChessGame>.GeneratePowerSet(x);
                    candidateRules.AddRange(powerSet.Where(set => !set.IsEmpty() && set.Items.Count < x.Items.Count).Select(set => 
                    {
                        if (targetItemset.IsEmpty())
                        {
                            return new AssociationRule<ChessGame>(set, x.Not(set));
                        }
                        return new AssociationRule<ChessGame>(set, targetItemset);
                        
                    }));
                    
                });
            return candidateRules;
        }
    }
}
 