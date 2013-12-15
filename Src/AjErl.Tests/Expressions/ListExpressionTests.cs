namespace AjErl.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ListExpressionTests
    {
        [TestMethod]
        public void CreateSimpleListWithVariable()
        {
            Context context = new Context();
            var expr = new ListExpression(new IExpression[] { new ConstantExpression(1), new VariableExpression(new Variable("X")), new AtomExpression(new Atom("y")) });

            Assert.IsTrue(expr.HasVariable());

            var result = expr.Evaluate(context, true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));

            var list = (List)result;

            Assert.IsNotNull(list.Head);
            Assert.IsNotNull(list.Tail);

            Assert.AreEqual("[1, X, y]", list.ToString());
        }
    }
}
