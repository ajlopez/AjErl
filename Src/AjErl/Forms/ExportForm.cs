namespace AjErl.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ExportForm : IForm
    {
        private IList<string> names;

        public ExportForm(IList<string> names)
        {
            this.names = names;
        }

        public IList<string> Names { get { return this.names; } }

        public object Evaluate(Context context)
        {
            context.Module.AddExportNames(this.names);
            return null;
        }
    }
}
