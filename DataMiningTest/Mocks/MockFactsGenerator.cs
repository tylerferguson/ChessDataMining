using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMining.Mocks;

namespace DataMining
{
    class MockFactsGenerator : IFactsGenerator<string>
    {
        public List<IFact<string>> Generate(List<IFact<string>> excludedFacts, string transaction)
        {
            var result = new List<IFact<string>>();
            var arr = transaction.ToCharArray();
            foreach (var item in arr)
            {
                result.Add(new MockSimpleFact(item.ToString()));
            }
            return result;
        }
    }
}
