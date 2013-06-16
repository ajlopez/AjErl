namespace AjErl.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Language;

    [TestClass]
    public class TupleExpressionTests
    {
        [TestMethod]
        public void CreateEmptyTuple()
        {
            var expr = new TupleExpression(new IExpression[] { });

            Assert.IsFalse(expr.HasVariable());

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Tuple));

            var tuple = (Tuple)result;

            Assert.AreEqual(0, tuple.Arity);
        }

        [TestMethod]
        public void CreateSimpleTuple()
        {
            Context context = new Context();
            var expr = new TupleExpression(new IExpression[] { new ConstantExpression(1), new VariableExpression(new Variable("X")), new AtomExpression(new Atom("y"))});

            Assert.IsTrue(expr.HasVariable());

            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Tuple));

            var tuple = (Tuple)result;

            Assert.AreEqual(3, tuple.Arity);
            Assert.AreEqual(1, tuple.ElementAt(0));
            Assert.IsInstanceOfType(tuple.ElementAt(1), typeof(Variable));
            Assert.AreEqual("X", ((Variable)tuple.ElementAt(1)).Name);
            Assert.IsInstanceOfType(tuple.ElementAt(2), typeof(Atom));
            Assert.AreEqual("y", ((Atom)tuple.ElementAt(2)).Name);
        }
    }
}
