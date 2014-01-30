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
        private IList<string> export = new List<string>();

        public Module(Context parent) 
        {
            this.context = new Context(parent, this);
        }

        public Context Context { get { return this.context; } }

        public string Name { get { return this.name; } }

        public IList<string> ExportNames { get { return this.export; } }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void AddExportNames(IList<string> names)
        {
            this.export = this.export.Union(names).ToList();
        }
    }
}
