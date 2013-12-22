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

            Assert.AreEqual("[1,X,y]", list.ToString());
        }

        [TestMethod]
        public void CreateSimpleListWithBoundVariable()
        {
            Context context = new Context();
            context.SetValue("X", 2);
            var expr = new ListExpression(new IExpression[] { new ConstantExpression(1), new VariableExpression(new Variable("X")), new AtomExpression(new Atom("y")) });

            Assert.IsTrue(expr.HasVariable());

            var result = expr.Evaluate(context, true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));

            var list = (List)result;

            Assert.IsNotNull(list.Head);
            Assert.IsNotNull(list.Tail);

            Assert.AreEqual("[1,2,y]", list.ToString());
        }

        [TestMethod]
        public void RaiseWhenUnboundVariable()
        {
            Context context = new Context();
            var expr = new ListExpression(new IExpression[] { new ConstantExpression(1), new VariableExpression(new Variable("X")), new AtomExpression(new Atom("y")) });

            Assert.IsTrue(expr.HasVariable());

            try
            {
                expr.Evaluate(context, false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("variable 'X' is unbound", ex.Message);
            }
        }

        [TestMethod]
        public void CreateListWithTail()
        {
            Context context = new Context();
            var list = List.MakeList(new object[] { 3, 4 });
            context.SetValue("Tail", list);
            var expr = new ListExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) }, new VariableExpression(new Variable("Tail")));

            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[1,2,3,4]", result.ToString());
        }

        [TestMethod]
        public void CreateListWithVariableAsTail()
        {
            Context context = new Context();
            var expr = new ListExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) }, new VariableExpression(new Variable("Tail")));

            var result = expr.Evaluate(context, true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[1,2|Tail]", result.ToString());
        }

        [TestMethod]
        public void RaiseIfUnboundVariableAsTail()
        {
            Context context = new Context();
            var expr = new ListExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) }, new VariableExpression(new Variable("Tail")));

            try
            {
                expr.Evaluate(context, false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("variable 'Tail' is unbound", ex.Message);
            }
        }
    }
}
