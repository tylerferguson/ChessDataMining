﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Mocks
{
    public class MockThresholdFilterer : IThresholdFilterer<string>
    {
        public List<AssociationRule<string>> FilterByMinThresholds(IEnumerable<IFact<string>> targetFacts, Database<string> projectedDatabase, List<ItemSet<IFact<string>>> frequentPatterns, List<AssociationRule<string>> candidateRules, double relativeMinsup, double minconf)
        {
            return candidateRules;
        }
    }
}
