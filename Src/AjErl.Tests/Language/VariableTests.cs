namespace AjErl.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VariableTests
    {
        [TestMethod]
        public void CreateSimpleVariable()
        {
            Variable variable = new Variable("X");

            Assert.AreEqual("X", variable.Name);
        }

        [TestMethod]
        public void VariableToString()
        {
            Variable variable = new Variable("X");

            Assert.AreEqual("X", variable.ToString());
        }

        [TestMethod]
        public void FirstVariable()
        {
            Variable variable = new Variable("X");

            Assert.AreEqual(variable, variable.FirstVariable());
        }
    }
}
