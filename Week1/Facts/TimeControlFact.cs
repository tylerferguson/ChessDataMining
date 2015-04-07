using DataMining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDataMining.Facts
{
    public class TimeControlFact : IFact<ChessGame>
    {
        private ITimeControlCategoriser categoriser;
        
        public TimeControlFact(ITimeControlCategoriser categoriser, string time)
        {
            this.categoriser = categoriser;
            Value = categoriser.Categorise(time);
        }

        public override bool IsTrue(ChessGame transaction)
        {
            return categoriser.Categorise(transaction.TimeControl) == Value;
        }

        public override bool Implies(IFact<ChessGame> that)
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

        public override string ToString()
        {
            return "Time control is " + Value;
        }
    }
}
