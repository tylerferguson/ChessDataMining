using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1.Mocks
{
    class MockMiner : IFrequentPatternsMiner<string>
    {
        public bool ReceivedCorrectProjectedDatabase { get; set; }
        public bool ReceivedCorrectTargetDatabase { get; set; }

        List<string> expectedProjectedDB;
        List<string> expectedTargetDB;

        public void Setup(List<string> expectedProjectedDB, List<string> expectedTargetDB)
        {
            this.expectedProjectedDB = expectedProjectedDB;
            this.expectedTargetDB = expectedTargetDB;
        }

        public List<ItemSet<IFact<string>>> Mine(Database<string> database, double minsup)
        {
            throw new NotImplementedException();
        }

        public List<ItemSet<IFact<string>>> Mine(Database<string> projectedDatabase, Database<string> targetDatabase, double minsup)
        {
            ReceivedCorrectProjectedDatabase = projectedDatabase.Transactions.SequenceEqual(expectedProjectedDB);
            ReceivedCorrectTargetDatabase = targetDatabase.Transactions.SequenceEqual(expectedTargetDB);
            return new List<ItemSet<IFact<string>>>();
        }
    }
}
