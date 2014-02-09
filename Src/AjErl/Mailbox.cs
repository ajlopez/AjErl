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
        private Stack<BlockingCollection<object>> queues = new Stack<BlockingCollection<object>>();
        private BlockingCollection<object> savequeue;
        private bool wasrejected;

        public void Add(object message)
        {
            this.queue.Add(message);
        }

        // it should be called by only by the current process
        public object Take()
        {
            if (!this.wasrejected && this.savequeue != null)
            {
                this.queues.Push(this.savequeue);
                this.savequeue = null;
            }

            while (this.queues.Count > 0 && this.queues.Peek().Count == 0)
                this.queues.Pop();

            object message;

            if (this.queues.Count > 0)
                message = this.queues.Peek().Take();
            else
                message = this.queue.Take();

            this.wasrejected = false;
            return message;
        }

        // it should be called only by the current process
        public void Reject(object message)
        {
            if (this.savequeue == null)
                this.savequeue = new BlockingCollection<object>();

            this.savequeue.Add(message);
            this.wasrejected = true;
        }
    }
}
