using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week1
{
    public class SimpleFact : IFact<ChessGame>, IEquatable<SimpleFact>
    {
        public string PropertyName { get; set; }
        public string Value { get; set; }

        public SimpleFact(string propertyName, string value)
        {
            this.PropertyName = propertyName;
            this.Value = value;
        }

        public bool IsTrue(ChessGame game)
        {
            string gameValue = game.GetType().GetProperty(this.PropertyName).GetValue(game).ToString();
            return Value.Equals(gameValue);
        }

        public bool Implies(IFact<ChessGame> that)
        {
            if (that == null)
            {
                return false;
            }

            SimpleFact fact = that as SimpleFact;
            return Implies(fact);
        }

        public bool Implies(SimpleFact that)
        {
            if (that == null)
            {
                return false;
            }

            var index = this.Value.IndexOf(",");
            var value = index > -1 ? this.Value.Remove(index) : this.Value;
            return value.Equals(that.Value);
        }

        public bool Equals(SimpleFact that)
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
            SimpleFact fact = obj as SimpleFact;
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
            SimpleFact that = fact as SimpleFact;

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
