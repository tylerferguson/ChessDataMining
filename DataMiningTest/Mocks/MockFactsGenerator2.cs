using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Mocks;

namespace DataMining
{
    class MockFactsGenerator2 : IFactsGenerator<string>
    {
        public List<IFact<string>> Generate(List<IFact<string>> excludedFacts, string transaction)
        {
            var index = transaction.IndexOf(" ");
            var value = index > -1 ? transaction.Remove(0, index + 1) : transaction;
            var result = new List<IFact<string>>() 
            {
                new MockOpeningFact(transaction)
            };

            if (value != transaction)
            {
                result.Add(new MockOpeningFact(value));
            }

            return result;
        }
    }
}
