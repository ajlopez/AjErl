namespace AjErl
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Mailbox
    {
        private BlockingCollection<object> queue = new BlockingCollection<object>();

        public void Add(object message)
        {
            this.queue.Add(message);
        }

        public object Take()
        {
            return this.queue.Take();
        }
    }
}
