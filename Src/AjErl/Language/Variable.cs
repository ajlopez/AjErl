namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Variable : IElement
    {
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
    }
}
