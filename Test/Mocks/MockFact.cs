using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1.Mocks
{
    public class MockFact : IFact<string>, IEquatable<MockFact>
    {
        public string Value { get; set; } 

        public MockFact(string value)
        {
            this.Value = value;
        }

        public bool IsTrue(string transaction)
        {
            return transaction.Contains(Value);
        }

        public int CompareTo(IFact<string> that)
        {
            if (that == null)
            {
                return 1;
            }
            MockFact fact = that as MockFact;

            return CompareTo(fact);
        }

        public int CompareTo(MockFact that)
        {
            if (that == null)
            {
                return 1;
            }
            return this.Value.CompareTo(that.Value);
        }

        public bool Implies(IFact<string> that)
        {
            if (that == null)
            {
                return false;
            }

            MockFact fact = that as MockFact;
            return Implies(fact);
        }

        public bool Implies(MockFact that)
        {
            if (that == null)
            {
                return false;
            }

            return this.Value.Equals(that.Value);
        }

        public bool Equals(MockFact that)
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
            MockFact fact = obj as MockFact;
            if (fact == null)
            {
                return false;
            }
            else
            {
                return Equals(fact);
            }
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
