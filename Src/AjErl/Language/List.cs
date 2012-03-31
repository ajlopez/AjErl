namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class List : IElement
    {
        private object head;
        private object tail;

        public List(object head, object tail)
        {
            this.head = head;
            this.tail = tail;
        }

        public object Head { get { return this.head; } }

        public object Tail { get { return this.tail; } }

        public bool Match(List list, Context context)
        {
            if (list == null)
                return false;

            var result = AjErl.Match.MatchObjects(this.head, list.Head, context);

            if (!result)
                return false;

            return AjErl.Match.MatchObjects(this.tail, list.Tail, context);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");

            builder.Append(this.head.ToString());

            object rest = this.tail;

            while (rest is List)
            {
                builder.Append(", ");
                List list = (List)rest;
                builder.Append(list.Head);
                rest = list.tail;
            }

            if (rest != null)
            {
                builder.Append("|");
                builder.Append(rest.ToString());
            }

            builder.Append("]");

            return builder.ToString();
        }

        public Variable FirstVariable()
        {
            Variable result = null;

            if (this.head is IElement)
            {
                result = (Variable)this.head;
                if (result != null)
                    return result;
            }

            if (this.tail is IElement) 
                result = ((IElement)this.tail).FirstVariable();

            return result;
        }
    }
}
