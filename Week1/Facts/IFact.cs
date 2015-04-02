using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week1
{
    public abstract class IFact<T>
    {
        public string Value { get; set; } 

        public abstract bool IsTrue(T transaction);
        public abstract bool Implies(IFact<T> that);
        public override abstract bool Equals(Object that);
        public override abstract string ToString();

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode() + Value.GetHashCode();
        }

        public int CompareTo(IFact<T> that)
        {
            if (that == null)
                return -1;
            
            if (!Object.ReferenceEquals(this.GetType(), that.GetType()))
                return this.GetType().ToString().CompareTo(that.GetType().ToString());
            else
                return CompareSameFact(that);
        }

        public virtual int CompareSameFact(IFact<T> that)
        {
            return this.Value.CompareTo(that.Value);
        }
    }
}
