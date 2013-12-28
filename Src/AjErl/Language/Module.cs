namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Module
    {
        private Context context;
        private string name;

        public Module(Context parent) 
        {
            this.context = new Context(parent, this);
        }

        public Context Context { get { return this.context; } }

        public string Name { get { return this.name; } }

        public void SetName(string name)
        {
            this.name = name;
        }
    }
}
