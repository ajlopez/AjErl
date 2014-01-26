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

        [TestMethod]
        public void Equals()
        {
            Variable variable1 = new Variable("X");
            Variable variable2 = new Variable("Y");
            Variable variable3 = new Variable("X");

            Assert.AreEqual(variable1, variable3);
            Assert.AreEqual(variable3, variable1);

            Assert.AreEqual(variable1.GetHashCode(), variable3.GetHashCode());

            Assert.AreNotEqual(variable1, null);
            Assert.AreNotEqual(variable1, 123);
            Assert.AreNotEqual(variable1, "foo");

            Assert.AreNotEqual(variable1, variable2);
            Assert.AreNotEqual(variable2, variable1);
        }
    }
}
