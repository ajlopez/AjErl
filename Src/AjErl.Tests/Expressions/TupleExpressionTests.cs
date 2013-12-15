namespace AjErl.Tests.Expressions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            context.SetValue("X", 2);
            var expr = new TupleExpression(new IExpression[] { new ConstantExpression(1), new VariableExpression(new Variable("X")), new AtomExpression(new Atom("y")) });

            Assert.IsTrue(expr.HasVariable());

            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Tuple));

            var tuple = (Tuple)result;

            Assert.AreEqual(3, tuple.Arity);
            Assert.AreEqual(1, tuple.ElementAt(0));
            Assert.AreEqual(2, tuple.ElementAt(1));
            Assert.IsInstanceOfType(tuple.ElementAt(2), typeof(Atom));
            Assert.AreEqual("y", ((Atom)tuple.ElementAt(2)).Name);
        }

        [TestMethod]
        public void RaiseIfTupleHasVariable()
        {
            Context context = new Context();
            var expr = new TupleExpression(new IExpression[] { new ConstantExpression(1), new VariableExpression(new Variable("X")), new AtomExpression(new Atom("y")) });

            try
            {
                expr.Evaluate(context);
                Assert.Fail();
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("variable 'X' is unbound", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIfTupleHasTupleWithVariable()
        {
            Context context = new Context();
            var expr = new TupleExpression(new IExpression[] { new ConstantExpression(1), new TupleExpression(new IExpression[] { new VariableExpression(new Variable("X")) }), new AtomExpression(new Atom("y")) });

            try
            {
                expr.Evaluate(context);
                Assert.Fail();
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("variable 'X' is unbound", ex.Message);
            }
        }
    }
}
