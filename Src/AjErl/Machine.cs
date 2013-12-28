﻿namespace AjErl
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Language;

    public class Machine
    {
        private Context rootcontext;

        public Machine()
        {
            this.rootcontext = new Context();
        }

        public Context RootContext { get { return this.rootcontext; } }

        public Module LoadModule(string modname)
        {
            Module module = new Module(this.rootcontext);
            Parser parser = new Parser(File.OpenText(modname + ".erl"));

            for (var form = parser.ParseForm(); form != null; form = parser.ParseForm())
                form.Evaluate(module.Context);

            return module;
        }
    }
}
