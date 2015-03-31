using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1.Facts
{
    public class TimeControlFact : IFact<ChessGame>
    {
        private string propertyName = "TimeControl";

        public string PropertyName
        {
            get { return propertyName; }
        }

        public string Value { get; set; }
        private ITimeControlCategoriser categoriser;
        
        public TimeControlFact(ITimeControlCategoriser categoriser, string time)
        {
            this.categoriser = categoriser;
            Value = categoriser.Categorise(time);
        }

        public bool IsTrue(ChessGame transaction)
        {
            return categoriser.Categorise(transaction.TimeControl) == Value;
        }

        public int CompareTo(IFact<ChessGame> that)
        {
            if (that == null)
            {
                return 1;
            }
            TimeControlFact fact = that as TimeControlFact;
            return CompareTo(fact);
        }

        public int CompareTo(TimeControlFact that)
        {
            if (that == null)
            {
                return 1;
            }
            return Value.CompareTo(that.Value);

        }

        public bool Implies(IFact<ChessGame> that)
        {
            return Equals(that);
        }

        public bool Equals(TimeControlFact that)
        {
            if (that == null)
            {
                return false;
            }
            
            return this.Value == that.Value;     
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            TimeControlFact fact = obj as TimeControlFact;

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
            return PropertyName.GetHashCode() + Value.GetHashCode();
        }

        public override string ToString()
        {
            return PropertyName + " is " + Value;
        }
    }
}
