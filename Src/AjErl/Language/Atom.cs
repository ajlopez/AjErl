namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Atom
    {
        private string name;

        public Atom(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public override string ToString()
        {
            return this.name;
        }

        public bool Match(Atom atom)
        {
            return atom != null && this.name == atom.Name;
        }
    }
}
