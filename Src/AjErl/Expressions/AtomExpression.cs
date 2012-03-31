namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class AtomExpression : IExpression
    {
        private Atom atom;

        public AtomExpression(Atom atom)
        {
            this.atom = atom;
        }

        public Atom Atom { get { return this.atom; } }

        public object Evaluate(Context context)
        {
            return this.atom;
        }

        public bool HasVariable()
        {
            return false;
        }
    }
}
