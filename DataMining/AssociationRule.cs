using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining
{
    public class AssociationRule<T> : IEquatable<AssociationRule<T>>
    {
        public ItemSet<IFact<T>> Left { get; set; }
        public ItemSet<IFact<T>> Right { get; set; }
        public int AbsoluteSupport { get; set; }
        public Double RelativeSupport { get; set; }
        public Double Confidence { get; set; }
        public Double LiftCorrelation { get; set; }

        public AssociationRule(ItemSet<IFact<T>> left, ItemSet<IFact<T>> right)
        {
            Left = left;
            Right = right;
        }

        public ItemSet<IFact<T>> Union()
        {
            return Left.Union(Right);
        }

        public bool Equals(AssociationRule<T> that)
        {
            if (that == null)
            {
                return false;
            }
            if (Left.Equals(that.Left) && Right.Equals(that.Right))
            {
                return true;
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
            AssociationRule<T> rule = obj as AssociationRule<T>;
            if (rule == null)
            {
                return false;
            }
            else
            {
                return Equals(rule);
            }
        }

        public override string ToString()
        {
            return "Given " + Left + " then " + Right + "\n" + " ( " + "support: " + AbsoluteSupport + ")" + 
                "\n" + "Probablity Before: " + Right.RelativeSupport + ", After: " + Confidence + ", " 
                + "Lift correlation : " + LiftCorrelation + ")" + "\n";
        }
    }
}
