﻿namespace AjErl.Tests
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
            Assert.IsNotNull(machine.RootContext.GetValue("c/1"));
            Assert.IsNotNull(machine.RootContext.GetValue("spawn/1"));
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
    }
}
