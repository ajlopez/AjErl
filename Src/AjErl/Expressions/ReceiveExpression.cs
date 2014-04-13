namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class ReceiveExpression : IExpression
    {
        private IList<MatchBody> matches;

        public ReceiveExpression(IList<MatchBody> matches)
        {
            this.matches = matches;
        }

        public IList<MatchBody> Matches { get { return this.matches; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            object message = Process.Current.GetMessage();

            foreach (var match in this.matches)
            {
                var newcontext = match.MakeContext(message, context);

                if (newcontext != null)
                    return match.Evaluate(newcontext);
            }

            Process.Current.RejectMessage(message);

            return null;
        }

        public bool HasVariable()
        {
            return false;
        }
    }
}
