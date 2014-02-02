namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;

    public class MatchBody
    {
        private Context context;
        private object head;
        private IExpression body;

        public MatchBody(Context context, object head, IExpression body)
        {
            this.context = context;
            this.head = head;
            this.body = body;
        }

        public object Head { get { return this.head; } }

        public IExpression Body { get { return this.body; } }

        public Context MakeContext(object argument)
        {
            Context newcontext = new Context();

            if (!MatchUtilities.MatchObjects(this.head, argument, newcontext))
                return null;

            newcontext.SetParent(this.context);
            return newcontext;
        }

        public object Evaluate(Context context)
        {
            return this.body.Evaluate(context);
        }
    }
}
