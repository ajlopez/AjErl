namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Variable
    {
        private string name;

        public Variable(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }
    }
}
