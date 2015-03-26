﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class AssociationRuleGenerator<T>
    {
        Database<T> database;
        IFrequentPatternsMiner<T> frequentPatternsMiner;
        ICandidateRuleGenerator<T> candidateRuleGenerator;

        public AssociationRuleGenerator(Database<T> database, IFrequentPatternsMiner<T> frequentPatternsMiner, ICandidateRuleGenerator<T> candidateRuleGenerator)
        {
            this.database = database;
            this.frequentPatternsMiner = frequentPatternsMiner;
            this.candidateRuleGenerator = candidateRuleGenerator;
        }
        
        public List<AssociationRule<T>> Generate(Double relativeMinsup, Double minconf, List<IFact<T>> projectionFacts = null, List<IFact<T>> targetFacts = null ) 
        {
            Database<T> projectedDatabase = database;

            if (projectionFacts != null)
            {
                foreach (var fact in projectionFacts)
                {
                    projectedDatabase = database.Project(fact);
                }
            }

            Database<T> targetDatabase = projectedDatabase;

            if (targetFacts != null)
            {
                foreach (var fact in targetFacts)
                {
                    targetDatabase = projectedDatabase.Project(fact);
                }
            }
 
            List<ItemSet<IFact<T>>> frequentPatterns = frequentPatternsMiner.Mine(projectedDatabase, targetDatabase, relativeMinsup);
            List<AssociationRule<T>> candidateRules = GenerateCandidateRules(targetFacts, frequentPatterns);

            candidateRules = FilterByMinThresholds(targetFacts, projectedDatabase, frequentPatterns, candidateRules, relativeMinsup, minconf);
            return candidateRules.OrderByDescending(rule => rule.LiftCorrelation).ToList();
        }

        private List<AssociationRule<T>> FilterByMinThresholds(List<IFact<T>> targetFacts, Database<T> projectedDatabase, List<ItemSet<IFact<T>>> frequentPatterns, List<AssociationRule<T>> candidateRules, Double relativeMinsup, Double minconf)
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

        private List<AssociationRule<T>> GenerateCandidateRules(List<IFact<T>> targetFacts, List<ItemSet<IFact<T>>> frequentPatterns)
        {
            return candidateRuleGenerator.GenerateCandidateRules(targetFacts, frequentPatterns);
        }
    }
}
