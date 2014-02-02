namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class SendExpression : IExpression
    {
        private IExpression processexpr;
        private IExpression messageexpr;

        public SendExpression(IExpression processexpr, IExpression messageexpr)
        {
            this.processexpr = processexpr;
            this.messageexpr = messageexpr;
        }

        public IExpression ProcessExpression { get { return this.processexpr; } }

        public IExpression MessageExpression { get { return this.messageexpr; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            object procvalue = this.processexpr.Evaluate(context, false);
            object message = this.messageexpr.Evaluate(context, false);

            Process process = (Process)procvalue;

            process.Tell(message);

            return message;
        }

        public bool HasVariable()
        {
            return this.processexpr.HasVariable() || this.messageexpr.HasVariable();
        }
    }
}
