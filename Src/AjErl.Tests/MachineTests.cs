namespace AjErl.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Expressions;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MachineTests
    {
        [TestMethod]
        public void CreateMachineWithRootContext()
        {
            Machine machine = new Machine();

            Assert.IsNotNull(machine.RootContext);
            Assert.IsNotNull(machine.RootContext.GetValue("c/1"));
            Assert.IsNotNull(machine.RootContext.GetValue("spawn/1"));
            Assert.IsNotNull(machine.RootContext.GetValue("self/0"));

            Assert.IsNotNull(machine.TextWriter);
        }

        [TestMethod]
        public void MachineToString()
        {
            Assert.AreEqual("null", Machine.ToString(null));
            Assert.AreEqual("false", Machine.ToString(false));
            Assert.AreEqual("true", Machine.ToString(true));
            Assert.AreEqual("1", Machine.ToString(1));
            Assert.AreEqual("foo", Machine.ToString("foo"));
        }

        [TestMethod]
        public void CreateMachineWithInitialModules()
        {
            Machine machine = new Machine();

            Assert.IsNotNull(machine.RootContext);
            Assert.IsNotNull(machine.RootContext.GetValue("lists"));
            Assert.IsInstanceOfType(machine.RootContext.GetValue("lists"), typeof(Module));
            Assert.IsNotNull(machine.RootContext.GetValue("io"));
            Assert.IsInstanceOfType(machine.RootContext.GetValue("io"), typeof(Module));
        }

        [TestMethod]
        [DeploymentItem("Modules\\arith.erl")]
        public void LoadArithModule()
        {
            Machine machine = new Machine();

            var module = machine.LoadModule("arith");

            Assert.IsNotNull(module);
            Assert.IsNotNull(module.Context);
            Assert.AreEqual("arith", module.Name);

            Assert.IsNotNull(module.Context.GetValue("add/2"));
            Assert.IsInstanceOfType(module.Context.GetValue("add/2"), typeof(Function));

            Assert.IsNotNull(module.Context.GetValue("subtract/2"));
            Assert.IsInstanceOfType(module.Context.GetValue("subtract/2"), typeof(Function));

            Assert.IsNotNull(module.Context.GetValue("multiply/2"));
            Assert.IsInstanceOfType(module.Context.GetValue("multiply/2"), typeof(Function));

            Assert.IsNotNull(module.Context.GetValue("divide/2"));
            Assert.IsInstanceOfType(module.Context.GetValue("divide/2"), typeof(Function));
        }

        [TestMethod]
        [DeploymentItem("Modules\\fibo.erl")]
        public void LoadAndUseFiboModule()
        {
            Machine machine = new Machine();

            var module = machine.LoadModule("fibo");

            Assert.IsNotNull(module);
            Assert.IsNotNull(module.Context);
            Assert.AreEqual("fibo", module.Name);

            Assert.IsNotNull(module.Context.GetValue("fibo/1"));
            Assert.IsInstanceOfType(module.Context.GetValue("fibo/1"), typeof(MultiFunction));

            var ffunc = (MultiFunction)module.Context.GetValue("fibo/1");

            Assert.AreEqual(1, ffunc.Apply(null, new object[] { 0 }));
            Assert.AreEqual(1, ffunc.Apply(null, new object[] { 1 }));
            Assert.AreEqual(2, ffunc.Apply(null, new object[] { 2 }));
            Assert.AreEqual(3, ffunc.Apply(null, new object[] { 3 }));
            Assert.AreEqual(5, ffunc.Apply(null, new object[] { 4 }));
        }

        [TestMethod]
        [DeploymentItem("Modules\\tail.erl")]
        public void LoadAndUseTailModuleWithTailRecursion()
        {
            Machine machine = new Machine();

            var module = machine.LoadModule("tail");

            Assert.IsNotNull(module);
            Assert.IsNotNull(module.Context);
            Assert.AreEqual("tail", module.Name);

            Assert.IsNotNull(module.Context.GetValue("tail/2"));
            Assert.IsInstanceOfType(module.Context.GetValue("tail/2"), typeof(MultiFunction));

            var ffunc = (MultiFunction)module.Context.GetValue("tail/2");

            var result = ffunc.Apply(null, new object[] { 2, 1 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DelayedCall));

            Assert.AreEqual(3, Machine.ExpandDelayedCall(result));
        }

        [TestMethod]
        [DeploymentItem("Modules\\area_server.erl")]
        public void LoadAreaServer()
        {
            Machine machine = new Machine();

            var module = machine.LoadModule("area_server");

            Assert.IsNotNull(module);
            Assert.IsNotNull(module.Context);
            Assert.AreEqual("area_server", module.Name);

            Assert.IsNotNull(module.Context.GetValue("loop/0"));

            machine.RootContext.SetValue(module.Name, module);

            this.EvaluateExpression("Pid = spawn(fun() -> area_server:loop() end).", machine.RootContext);

            Process process = new Process();
            Process.Current = process;

            this.EvaluateExpression("Pid ! {self(), rectangle, 6, 10}.", machine.RootContext);
            Assert.AreEqual(60, process.GetMessage());

            this.EvaluateExpression("Pid ! {self(), circle, 23}.", machine.RootContext);
            Assert.AreEqual(3.14159 * 23 * 23, process.GetMessage());

            this.EvaluateExpression("Pid ! stop.", machine.RootContext);
        }

        private object EvaluateExpression(string text, Context context)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            return expression.Evaluate(context);
        }
    }
}
