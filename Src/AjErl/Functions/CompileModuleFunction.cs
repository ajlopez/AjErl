namespace AjErl.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class CompileModuleFunction : IFunction
    {
        private Machine machine;

        public CompileModuleFunction(Machine machine)
        {
            this.machine = machine;
        }

        public object Apply(IList<object> arguments)
        {
            Atom atom = (Atom)arguments[0];
            Module module = this.machine.LoadModule(atom.Name);
            Tuple tuple = new Tuple(new object[] { new Atom("ok"), new Atom(module.Name) });
            return tuple;
        }
    }
}
