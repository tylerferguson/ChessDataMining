using DataMining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessDataMining.Facts
{
    public class SimpleFact : IFact<ChessGame>, IEquatable<SimpleFact>
    {
        public string PropertyName { get; set; }
        public enum validParam { White, Black, Result, Day};

        public SimpleFact(string propertyName, string value)
        {
            validParam enumPropertyName;
            var parseResult = Enum.TryParse(propertyName, out enumPropertyName);

            if (parseResult)
            {
                PropertyName = propertyName;
                this.Value = value;
            }
            else
            {
                throw new ArgumentException();
            }
            
        }

        public override bool IsTrue(ChessGame game)
        {
            string gameValue = game.GetType().GetProperty(this.PropertyName).GetValue(game).ToString();
            return Value.Equals(gameValue);
        }

        public override bool Implies(IFact<ChessGame> that)
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

            return Equals(fact);
        }

        public override int CompareSameFact(IFact<ChessGame> that)
        {
            SimpleFact fact = that as SimpleFact;

            if (PropertyName == fact.PropertyName)
            {
                return this.Value.CompareTo(fact.Value);
            }
            return PropertyName.CompareTo(fact.PropertyName);
        }

        public override string ToString()
        {
            return PropertyName + " is " + Value;
        }
    }
}
