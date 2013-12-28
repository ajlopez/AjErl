namespace AjErl.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
        }

        [TestMethod]
        [DeploymentItem("Modules\\arith.erl")]
        public void LoadArithModule()
        {
            Machine machine = new Machine();

            var module = machine.LoadModule("arith");

            Assert.IsNotNull(module);
            Assert.IsNotNull(module.Context);

            Assert.IsNotNull(module.Context.GetValue("add/2"));
            Assert.IsInstanceOfType(module.Context.GetValue("add/2"), typeof(Function));

            Assert.IsNotNull(module.Context.GetValue("subtract/2"));
            Assert.IsInstanceOfType(module.Context.GetValue("subtract/2"), typeof(Function));

            Assert.IsNotNull(module.Context.GetValue("multiply/2"));
            Assert.IsInstanceOfType(module.Context.GetValue("multiply/2"), typeof(Function));

            Assert.IsNotNull(module.Context.GetValue("divide/2"));
            Assert.IsInstanceOfType(module.Context.GetValue("divide/2"), typeof(Function));
        }
    }
}
