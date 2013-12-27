namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Module
    {
        private Context context;

        public Module(Context parent) 
        {
            this.context = new Context(parent);
        }

        public Context Context { get { return this.context; } }
    }
}
