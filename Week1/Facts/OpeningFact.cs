using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week1
{
    public class OpeningFact : IFact<ChessGame>, IEquatable<OpeningFact>
    {
        private string propertyName = "Opening";

        public string PropertyName
        {
            get { return propertyName; }
        }

        public string Value { get; set; }

        public OpeningFact(string value)
        {
            this.Value = value;
        }

        public bool Implies(IFact<ChessGame> that)
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
            return this.Value.StartsWith(that.Value);
        }


        public bool IsTrue(ChessGame game)
        {
            string gameValue = game.GetType().GetProperty(this.PropertyName).GetValue(game).ToString();
            return gameValue.StartsWith(Value);
        }

        public bool Equals(OpeningFact that)
        {
            if (that == null)
            {
                return false;
            }
            if (this.PropertyName.Equals(that.PropertyName))
            {
                return this.Value.Equals(that.Value);
            }
            else
            {
                return false;
            }
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

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode() + Value.GetHashCode();
        }

        public int CompareTo(IFact<ChessGame> fact)
        {
            OpeningFact that = fact as OpeningFact;

            if (that == null)
                return -1;

            if (this.PropertyName.Equals(that.PropertyName))
            {
                return this.Value.CompareTo(that.Value);
            }
            return this.PropertyName.CompareTo(that.PropertyName);
        }

        public override string ToString()
        {
            return PropertyName + " is " + Value;
        }
    }
}
