namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Variable : IElement
    {
        private static int hashcode = typeof(Variable).GetHashCode();
        private string name;

        public Variable(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public override string ToString()
        {
            return this.name;
        }

        public Variable FirstVariable()
        {
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Variable))
                return false;

            return this.name.Equals(((Variable)obj).Name);
        }

        public override int GetHashCode()
        {
            return hashcode + this.name.GetHashCode();
        }
    }
}
