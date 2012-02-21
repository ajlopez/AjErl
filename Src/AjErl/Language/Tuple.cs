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
    }
}
