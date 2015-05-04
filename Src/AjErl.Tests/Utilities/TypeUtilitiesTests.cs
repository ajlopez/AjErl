namespace AjErl.Tests.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Compiler;
    using AjErl.Tests.Classes;
    using AjErl.Utilities;

    [TestClass]
    public class TypeUtilitiesTests
    {
        [TestMethod]
        public void GetTypeByName()
        {
            Type type = TypeUtilities.GetType("System.Int32");

            Assert.IsNotNull(type);
            Assert.AreEqual(typeof(int), type);
        }

        [TestMethod]
        public void GetLibraryTypeByName()
        {
            Type type = TypeUtilities.GetType("AjErl.Context");

            Assert.IsNotNull(type);
            Assert.AreEqual(typeof(Context), type);
        }

        [TestMethod]
        public void GetConsoleTypeByName()
        {
            Type type = TypeUtilities.GetType("AjErl.Console.Program");

            Assert.IsNotNull(type);
            Assert.AreEqual("AjErl.Console.Program", type.Name);
        }
    }
}
