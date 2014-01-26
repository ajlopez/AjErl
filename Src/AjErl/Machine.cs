namespace AjErl
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Functions;
    using AjErl.Language;
    using AjErl.Modules;

    public class Machine
    {
        private Context rootcontext;

        public Machine()
        {
            this.rootcontext = new Context();
            this.rootcontext.SetValue("c/1", new CompileModuleFunction(this));
            this.rootcontext.SetValue("spawn/1", new SpawnFunction());

            Module lists = new ListsModule(this.rootcontext);
            this.rootcontext.SetValue(lists.Name, lists);
        }

        public Context RootContext { get { return this.rootcontext; } }

        public static object ExpandDelayedCall(object value)
        {
            while (value is DelayedCall)
                value = ((DelayedCall)value).Evaluate();

            return value;
        }

        public Module LoadModule(string modname)
        {
            Module module = new Module(this.rootcontext);
            StreamReader reader = File.OpenText(modname + ".erl");
            Parser parser = new Parser(reader);

            for (var form = parser.ParseForm(); form != null; form = parser.ParseForm())
                form.Evaluate(module.Context);

            reader.Close();

            return module;
        }

        public static string ToString(object value)
        {
            if (value.Equals(false))
                return "false";
            if (value.Equals(true))
                return "true";
            if (value == null)
                return "null";

            return value.ToString();
        }
    }
}
