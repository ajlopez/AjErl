namespace AjErl.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using AjErl.Language;

    public class MultiFunctionForm : IForm
    {
        private IList<FunctionForm> forms;

        public MultiFunctionForm(IList<FunctionForm> forms)
        {
            this.forms = forms;
        }

        public IList<FunctionForm> Forms { get { return this.forms; } }

        public object Evaluate(Context context)
        {
            string name = this.forms[0].Name;
            int arity = this.forms[0].ParameterExpressions.Count;

            if (!forms.Skip(1).All(f => f.Name == name && f.ParameterExpressions.Count == arity))
                throw new Exception("head mismatch");

            IList<Function> functions = new List<Function>();

            foreach (var form in this.Forms)
                functions.Add((Function)form.Evaluate(context));

            var func = new MultiFunction(functions);

            context.SetValue(string.Format("{0}/{1}", name, arity), func);

            return func;
        }
    }
}
