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

        public static List MakeList(IList<object> elements, List tail = null)
        {
            if (elements.Count == 0)
                return tail;

            return new List(elements[0], MakeList(elements.Skip(1).ToList(), tail));
        }

        public static List MakeList(IList<object> elements, Variable tail)
        {
            if (elements.Count == 1)
                return new List(elements[0], tail);

            return new List(elements[0], MakeList(elements.Skip(1).ToList(), tail));
        }

        public bool Match(List list, Context context)
        {
            if (list == null)
                return false;

            var result = AjErl.MatchUtilities.MatchObjects(this.head, list.Head, context);

            if (!result)
                return false;

            return AjErl.MatchUtilities.MatchObjects(this.tail, list.Tail, context);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");

            builder.Append(Machine.ToString(this.head));

            object rest = this.tail;

            while (rest is List)
            {
                builder.Append(",");
                List list = (List)rest;
                builder.Append(Machine.ToString(list.Head));
                rest = list.tail;
            }

            if (rest != null)
            {
                builder.Append("|");
                builder.Append(Machine.ToString(rest));
            }

            builder.Append("]");

            return builder.ToString();
        }

        public Variable FirstVariable()
        {
            Variable result = null;

            if (this.head is IElement)
            {
                result = ((IElement)this.head).FirstVariable();
                if (result != null)
                    return result;
            }

            if (this.tail is IElement) 
                result = ((IElement)this.tail).FirstVariable();

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is List))
                return false;

            var list0 = this;
            var list = (List)obj;

            while (true)
            {
                if (list == null)
                    return false;

                if (!Machine.AreEqual(list0.Head, list.Head))
                    return false;

                if (!(list0.Tail is List))
                    return Machine.AreEqual(list0.Tail, list.Tail);

                list0 = (List)list0.Tail;
                list = (List)list.Tail;
            }
        }

        public override int GetHashCode()
        {
            int result = 0;
            var list = this;

            while (list != null) 
            {
                result *= 17;
                result += Machine.GetHashCode(list.Head);

                if (!(list.Tail is List))
                {
                    result *= 17;
                    result += Machine.GetHashCode(list.Tail);
                    break;
                }

                list = (List)list.Tail;
            }

            return result;
        }
    }
}
