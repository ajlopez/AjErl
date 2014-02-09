namespace AjErl.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Functions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SelfFunctionTests
    {
        [TestMethod]
        public void GetCurrentProcess()
        {
            Process process = new Process();
            Process.Current = process;
            SelfFunction func = new SelfFunction();

            var result = func.Apply(null, null);

            Assert.IsNotNull(result);
            Assert.AreSame(process, result);
        }
    }
}
