using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week1.Mocks;

namespace Week1
{
    class MockFactsGenerator : IFactsGenerator<string>
    {
        public List<IFact<string>> Generate(List<IFact<string>> excludedFacts, string transaction)
        {
            var result = new List<IFact<string>>();
            var arr = transaction.ToCharArray();
            foreach (var item in arr)
            {
                result.Add(new MockFact(item.ToString()));
            }
            return result;
        }
    }
}
