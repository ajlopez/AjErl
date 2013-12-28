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

            Assert.IsNotNull(module.Context.GetValue("add"));
            Assert.IsInstanceOfType(module.Context.GetValue("add"), typeof(Function));

            Assert.IsNotNull(module.Context.GetValue("subtract"));
            Assert.IsInstanceOfType(module.Context.GetValue("subtract"), typeof(Function));

            Assert.IsNotNull(module.Context.GetValue("multiply"));
            Assert.IsInstanceOfType(module.Context.GetValue("multiply"), typeof(Function));

            Assert.IsNotNull(module.Context.GetValue("divide"));
            Assert.IsInstanceOfType(module.Context.GetValue("divide"), typeof(Function));
        }
    }
}
