namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Atom
    {
        private static int hashcode = typeof(Atom).GetHashCode();
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Atom))
                return false;

            var atom = (Atom)obj;

            return this.name.Equals(atom.name);
        }

        public override int GetHashCode()
        {
            return hashcode + this.name.GetHashCode();
        }
    }
}
