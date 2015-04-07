using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining
{
    public class AssociationRuleGenerator<T>
    {
        Database<T> database;
        IFrequentPatternsMiner<T> frequentPatternsMiner;
        ICandidateRuleGenerator<T> candidateRuleGenerator;
        IThresholdFilterer<T> filterer;

        public AssociationRuleGenerator(Database<T> database, IFrequentPatternsMiner<T> frequentPatternsMiner, ICandidateRuleGenerator<T> candidateRuleGenerator, IThresholdFilterer<T> filterer)
        {
            this.database = database;
            this.frequentPatternsMiner = frequentPatternsMiner;
            this.candidateRuleGenerator = candidateRuleGenerator;
            this.filterer = filterer;
        }
        
        public List<AssociationRule<T>> Generate(Double relativeMinsup, Double minconf, List<IFact<T>> projectionFacts = null, List<IFact<T>> targetFacts = null ) 
        {
            Database<T> projectedDatabase = database;

            if (projectionFacts != null)
            {
                foreach (var fact in projectionFacts)
                {
                    projectedDatabase = projectedDatabase.Project(fact);
                }
            }

            Database<T> targetDatabase = projectedDatabase;

            if (targetFacts != null)
            {
                foreach (var fact in targetFacts)
                {
                    targetDatabase = targetDatabase.Project(fact);
                }
            }
 
            List<ItemSet<IFact<T>>> frequentPatterns = frequentPatternsMiner.Mine(projectedDatabase, targetDatabase, relativeMinsup);
            List<AssociationRule<T>> candidateRules = GenerateCandidateRules(targetFacts, frequentPatterns);

            candidateRules = FilterByMinThresholds(targetFacts, projectedDatabase, frequentPatterns, candidateRules, relativeMinsup, minconf);
            return candidateRules.OrderByDescending(rule => rule.LiftCorrelation).ToList();
        }

        private List<AssociationRule<T>> FilterByMinThresholds(List<IFact<T>> targetFacts, Database<T> projectedDatabase, List<ItemSet<IFact<T>>> frequentPatterns, List<AssociationRule<T>> candidateRules, Double relativeMinsup, Double minconf)
        {
            return filterer.FilterByMinThresholds(targetFacts, projectedDatabase, frequentPatterns, candidateRules, relativeMinsup, minconf);
        }

        private List<AssociationRule<T>> GenerateCandidateRules(List<IFact<T>> targetFacts, List<ItemSet<IFact<T>>> frequentPatterns)
        {
            return candidateRuleGenerator.GenerateCandidateRules(targetFacts, frequentPatterns);
        }
    }
}
