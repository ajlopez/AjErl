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

        public Context Match(Tuple tuple, Context context)
        {
            if (tuple == null)
                return null;

            if (tuple.Arity != this.Arity)
                return null;

            for (int k = 0; k < this.elements.Count && context != null; k++)
                context = AjErl.Match.MatchObjects(this.elements[k], tuple.ElementAt(k), context);

            return context;
        }
    }
}
