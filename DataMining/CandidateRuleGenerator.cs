using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining
{
    public class CandidateRuleGenerator<T> : ICandidateRuleGenerator<T>
    {
        public List<AssociationRule<T>> GenerateCandidateRules(List<ItemSet<IFact<T>>> frequentPatterns)
        {
            var candidateRules = new List<AssociationRule<T>>();
            frequentPatterns.ForEach(x =>
            {
                var powerSet = PowerSetGenerator<T>.GeneratePowerSet(x);

                candidateRules.AddRange(powerSet.Where(set => !set.IsEmpty() && set.Items.Count < x.Items.Count).Select(set =>
                {
                    return new AssociationRule<T>(set, x.Not(set));
                }));

            });
            return candidateRules;
        }

        public List<AssociationRule<T>> GenerateCandidateRules(IEnumerable<IFact<T>> targetFacts, List<ItemSet<IFact<T>>> frequentPatterns)
        {
            if (targetFacts == null)
            {
                return GenerateCandidateRules(frequentPatterns);
            }

            var candidateRules = new List<AssociationRule<T>>();
            frequentPatterns.ForEach(x => 
                {
                    var powerSet = PowerSetGenerator<T>.GeneratePowerSet(x);

                    candidateRules.AddRange(powerSet.Where(set => !set.IsEmpty() && !candidateRules.Any(rule => rule.Left.Equals(set))).Select(set => 
                    {
                        var targetItemset = new ItemSet<IFact<T>>(targetFacts);
                        return new AssociationRule<T>(set, targetItemset);
                        
                    }));
                    
                });
            return candidateRules;
        }
    }
}
 