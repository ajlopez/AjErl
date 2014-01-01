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
    public class QualifiedCallExpressionTests
    {
        [TestMethod]
        public void EvaluateQualifiedCallExpression()
        {
            Context context = new Context();
            Module module = new Module(null);
            context.SetValue("mod", module);
            Function function = new Function(context, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            module.Context.SetValue("add/2", function);
            QualifiedCallExpression expr = new QualifiedCallExpression(new AtomExpression(new Atom("mod")), new AtomExpression(new Atom("add")), new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });

            Assert.IsFalse(expr.HasVariable());
            Assert.IsNotNull(expr.ModuleExpression);
            Assert.IsInstanceOfType(expr.ModuleExpression, typeof(AtomExpression));
            Assert.IsNotNull(expr.NameExpression);
            Assert.IsInstanceOfType(expr.NameExpression, typeof(AtomExpression));
            Assert.IsNotNull(expr.ArgumentExpressions);
            Assert.AreEqual(2, expr.ArgumentExpressions.Count);

            Assert.AreEqual(3, expr.Evaluate(context));
        }
    }
}
