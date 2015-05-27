using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Mocks
{
    public class MockSimpleFact : IFact<string>, IEquatable<MockSimpleFact>
    {
        public MockSimpleFact(string value)
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

            MockSimpleFact fact = that as MockSimpleFact;
            return Implies(fact);
        }

        public bool Implies(MockSimpleFact that)
        {
            if (that == null)
            {
                return false;
            }

            return this.Value.Equals(that.Value);
        }

        public bool Equals(MockSimpleFact that)
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
            MockSimpleFact fact = obj as MockSimpleFact;
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
