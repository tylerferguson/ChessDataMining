using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Mocks
{
    public class MockOpeningFact : IFact<string>, IEquatable<MockOpeningFact>
    {
        public MockOpeningFact(string value)
        {
            this.Value = value;
        }

        public override bool IsTrue(string transaction)
        {
            return transaction.Contains(Value);
        }

        public override bool Implies(IFact<string> that)
        {
            if (that == null)
            {
                return false;
            }

            MockOpeningFact fact = that as MockOpeningFact;
            return Implies(fact);
        }

        public bool Implies(MockOpeningFact that)
        {
            if (that == null)
            {
                return false;
            }

            return this.Value.Contains(that.Value);
        }

        public bool Equals(MockOpeningFact that)
        {
            if (that == null)
            {
                return false;
            }
            return this.Value.Equals(that.Value);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            MockOpeningFact fact = obj as MockOpeningFact;
            if (fact == null)
            {
                return false;
            }
            else
            {
                return Equals(fact);
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
