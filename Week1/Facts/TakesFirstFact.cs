using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1.Facts
{
    public class TakesFirstFact : IFact<ChessGame>
    {
        public TakesFirstFact(string moves)
        {
            if (moves == null || !moves.Contains("x"))
            {
                Value = "Neither";
            }
            else
            {
                var sections = moves.Split(' ').ToList();
                var section = sections.First(x => x.Contains("x"));
                var index = sections.IndexOf(section);
                if (index % 3 == 1)
                {
                    Value = "White";
                }
                else if (index % 3 == 2)
                {
                    Value = "Black";
                }
                else
                {
                    throw new ArgumentException();  
                }
            }
        }

        public override bool IsTrue(ChessGame transaction)
        {
            string value;
            if (transaction.Moves == null || !transaction.Moves.Contains("x"))
            {
                value = "Neither";
            }
            else
            {
                var sections = transaction.Moves.Split(' ').ToList();
                var section = sections.First(x => x.Contains("x"));
                var index = sections.IndexOf(section);

                if (index % 3 == 1)
                {
                    value = "White";
                }
                else if (index % 3 == 2)
                {
                    value = "Black";
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            return value == Value;
        }

        public override bool Implies(IFact<ChessGame> that)
        {
            return Equals(that);
        }

        public override bool Equals(object that)
        {
            if (that == null)
            {
                return false;
            }

            TakesFirstFact fact = that as TakesFirstFact;

            if (fact == null)
            {
                return false;
            }
            else
            {
                return Equals(fact);
            }
        }

        public bool Equals(TakesFirstFact that)
        {
            if (that == null)
            {
                return false;
            }

            return this.Value == that.Value;    
        }

        public override string ToString()
        {
            return Value + " takes first";
        }
    }
}
