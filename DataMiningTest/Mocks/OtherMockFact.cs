using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Mocks
{
    public class OtherMockFact : IFact<string>, IEquatable<OtherMockFact>
    {
        public OtherMockFact(string value)
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

            OtherMockFact fact = that as OtherMockFact;
            return Implies(fact);
        }

        public bool Implies(OtherMockFact that)
        {
            if (that == null)
            {
                return false;
            }

            return this.Value.Equals(that.Value);
        }

        public bool Equals(OtherMockFact that)
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
            OtherMockFact fact = obj as OtherMockFact;
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
