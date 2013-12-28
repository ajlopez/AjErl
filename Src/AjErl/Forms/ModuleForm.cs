namespace AjErl.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ModuleForm : IForm
    {
        private string name;

        public ModuleForm(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public object Evaluate(Context context)
        {
            context.Module.SetName(this.name);
            return null;
        }
    }
}
