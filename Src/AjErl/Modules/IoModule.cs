namespace AjErl.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class IoModule : Module
    {
        private Machine machine;
        private Atom ok;

        public IoModule(Machine machine)
            : base(machine.RootContext)
        {
            this.machine = machine;
            this.ok = new Atom("ok");
            this.SetName("io");
            this.Context.SetValue("write/1", new FuncFunction(this.Write));
            this.AddExportNames(new string[] { "write/1" });
            this.Context.SetValue("nl/0", new FuncFunction(this.Nl));
            this.AddExportNames(new string[] { "nl/0" });
        }

        private object Write(Context context, IList<object> arguments)
        {
            this.machine.TextWriter.Write(Machine.ToString(arguments[0]));
            return this.ok;
        }

        private object Nl(Context context, IList<object> arguments)
        {
            this.machine.TextWriter.WriteLine();
            return this.ok;
        }
    }
}
