namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tuple
    {
        private IList<object> elements;

        public Tuple(IEnumerable<object> elements)
        {
            this.elements = new List<object>(elements);
        }

        public int Arity { get { return this.elements.Count; } }

        public object ElementAt(int position)
        {
            return this.elements[position];
        }

        public bool Match(Tuple tuple, Context context)
        {
            if (tuple == null)
                return false;

            if (tuple.Arity != this.Arity)
                return false;

            for (int k = 0; k < this.elements.Count && context != null; k++)
                if (!AjErl.Match.MatchObjects(this.elements[k], tuple.ElementAt(k), context))
                    return false;

            return true;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("{");

            for (int k = 0; k < this.elements.Count; k++) 
            {
                if (k > 0)
                    builder.Append(", ");

                builder.Append(this.elements[k].ToString());
            }

            builder.Append("}");

            return builder.ToString();
        }
    }
}
