namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class List
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

        public Context Match(List list, Context context)
        {
            if (list == null)
                return null;

            var result = AjErl.Match.MatchObjects(this.head, list.Head, context);

            if (result == null)
                return null;

            return AjErl.Match.MatchObjects(this.tail, list.Tail, result);
        }
    }
}
