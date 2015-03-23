using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week1
{
    public class ItemSet<T> : IEquatable<ItemSet<T>>
    {
        public List<T> Items { get; set; }

        public Double RelativeSupport { get; set; }

        private int absoluteSupport = 0;

        public int AbsoluteSupport
        {
            get { return absoluteSupport; }
            set { absoluteSupport = value; }
        }

        public ItemSet()
        {
            Items = new List<T>();
        }

        public ItemSet(T item)
        {
            Items = new List<T>() { item };
        }

        public ItemSet(IEnumerable<T> items)
        {
            Items = items.ToList();
        }

        public override string ToString()
        {
            String content = Items.First().ToString();
            foreach (var item in Items.Skip(1))
            {
                content += " and " + item;

            }

            return content;
        }

        public bool Contains(ItemSet<T> itemSet)
        {
            return Items.Intersect(itemSet.Items).Count() == itemSet.Items.Count();
        }

        public bool Equals(ItemSet<T> that)
        {
            if (that == null)
            {
                return false;
            }
            if (this.Contains(that) && that.Contains(this))
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
            ItemSet<T> itemSet = obj as ItemSet<T>;
            if (itemSet == null)
            {
                return false;
            }
            else
            {
                return Equals(itemSet);
            }
        }

        public ItemSet<T> Not(ItemSet<T> that)
        {
            return new ItemSet<T>(this.Items.Except(that.Items));
        }

        public ItemSet<T> Union(ItemSet<T> that)
        {
            return new ItemSet<T>(this.Items.Union(that.Items));
        }

        public ItemSet<T> Intersect(ItemSet<T> that)
        {
            return new ItemSet<T>(Items.Intersect(that.Items));
        }

        public bool IsEmpty()
        {
            return !this.Items.Any();
        } 
    }
}
