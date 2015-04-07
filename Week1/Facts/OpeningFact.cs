using DataMining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessDataMining
{
    public class OpeningFact : IFact<ChessGame>, IEquatable<OpeningFact>
    {
        public OpeningFact(string value)
        {
            Value = value;
        }

        public override bool Implies(IFact<ChessGame> that)
        {
            if (that == null)
            {
                return false;
            }

            OpeningFact fact = that as OpeningFact;
            return Implies(fact);
        }

        public bool Implies(OpeningFact that)
        {
            if (that == null)
            {
                return false;
            }
            return this.Value.Contains(that.Value);
        }


        public override bool IsTrue(ChessGame game)
        {
            return game.Opening.Contains(Value);
        }

        public bool Equals(OpeningFact that)
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
            OpeningFact fact = obj as OpeningFact;
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
            return "Opening is " + Value;
        }
    }
}
