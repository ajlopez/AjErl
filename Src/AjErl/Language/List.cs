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
    }
}
