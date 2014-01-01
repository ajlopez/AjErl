namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class QualifiedCallExpression : IExpression
    {
        private IExpression moduleexpression;
        private IExpression nameexpression;
        private IList<IExpression> argumentexpressions;

        public QualifiedCallExpression(IExpression moduleexpression, IExpression nameexpression, IList<IExpression> argumentexpressions)
        {
            this.moduleexpression = moduleexpression;
            this.nameexpression = nameexpression;
            this.argumentexpressions = argumentexpressions;
        }

        public IExpression ModuleExpression { get { return this.moduleexpression; } }

        public IExpression NameExpression { get { return this.nameexpression; } }

        public IList<IExpression> ArgumentExpressions { get { return this.argumentexpressions; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            object modulevalue = this.moduleexpression.Evaluate(context, withvars);
            object namevalue = this.nameexpression.Evaluate(context, withvars);

            string modulename = ((Atom)modulevalue).Name;
            string name = string.Format("{0}/{1}", ((Atom)namevalue).Name, this.argumentexpressions.Count);

            IList<object> arguments = new List<object>();

            foreach (var argexpr in this.argumentexpressions)
                arguments.Add(argexpr.Evaluate(context, withvars));

            Module module = (Module)context.GetValue(modulename);
            IFunction func = (IFunction)module.Context.GetValue(name);

            return func.Apply(context, arguments);
        }

        public bool HasVariable()
        {
            if (this.nameexpression.HasVariable())
                return true;

            foreach (var expr in this.argumentexpressions)
                if (expr.HasVariable())
                    return true;

            return false;
        }
    }
}
