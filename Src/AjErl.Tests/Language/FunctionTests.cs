namespace AjErl.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void EvaluateConstantBody()
        {
            Function function = new Function(new ConstantExpression(1));

            Assert.AreEqual(1, function.Evaluate(null));
        }
    }
}
