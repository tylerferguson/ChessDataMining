using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining
{
    public class ThresholdFilterer<T> : IThresholdFilterer<T>
    {
        public List<AssociationRule<T>> FilterByMinThresholds(List<IFact<T>> targetFacts, Database<T> projectedDatabase, List<ItemSet<IFact<T>>> frequentPatterns, List<AssociationRule<T>> candidateRules, Double relativeMinsup, Double minconf)
        {
            candidateRules.ForEach(candidateRule =>
            {
                var leftSet = frequentPatterns.Find(x => x.Equals(candidateRule.Left));
                ItemSet<IFact<T>> rightSet;
                ItemSet<IFact<T>> unionSet;

                if (targetFacts == null)
                {
                    rightSet = frequentPatterns.Find(x => x.Equals(candidateRule.Right));
                    unionSet = frequentPatterns.Find(x => x.Equals(candidateRule.Union()));

                    candidateRule.AbsoluteSupport = unionSet.AbsoluteSupport;
                    candidateRule.RelativeSupport = unionSet.RelativeSupport;
                    candidateRule.Left.RelativeSupport = leftSet.RelativeSupport;
                    candidateRule.Right.RelativeSupport = rightSet.RelativeSupport;
                }
                else
                {
                    candidateRule.AbsoluteSupport = leftSet.AbsoluteSupport;
                    candidateRule.RelativeSupport = leftSet.RelativeSupport;
                    candidateRule.Left.RelativeSupport = projectedDatabase.CalculateSupport(candidateRule.Left);
                    candidateRule.Right.RelativeSupport = projectedDatabase.CalculateSupport(candidateRule.Right);
                }
                candidateRule.Confidence = candidateRule.RelativeSupport / candidateRule.Left.RelativeSupport;
                candidateRule.LiftCorrelation = candidateRule.Confidence / candidateRule.Right.RelativeSupport;

            });

            return candidateRules.Where(rule => rule.RelativeSupport >= relativeMinsup && rule.Confidence >= minconf).ToList(); 
        }
        
    }
}
