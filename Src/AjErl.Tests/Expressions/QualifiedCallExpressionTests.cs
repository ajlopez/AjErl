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
            module.AddExportNames(new string[] { "add/2" });
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

        [TestMethod]
        public void EvaluateQualifiedCallExpressionWithVariableInModuleName()
        {
            Context context = new Context();
            Module module = new Module(null);
            context.SetValue("mod", module);
            context.SetValue("M", new Atom("mod"));
            Function function = new Function(context, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            module.Context.SetValue("add/2", function);
            module.AddExportNames(new string[] { "add/2" });
            QualifiedCallExpression expr = new QualifiedCallExpression(new VariableExpression(new Variable("M")), new AtomExpression(new Atom("add")), new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });

            Assert.IsTrue(expr.HasVariable());
            Assert.IsNotNull(expr.ModuleExpression);
            Assert.IsInstanceOfType(expr.ModuleExpression, typeof(VariableExpression));
            Assert.IsNotNull(expr.NameExpression);
            Assert.IsInstanceOfType(expr.NameExpression, typeof(AtomExpression));
            Assert.IsNotNull(expr.ArgumentExpressions);
            Assert.AreEqual(2, expr.ArgumentExpressions.Count);

            Assert.AreEqual(3, expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateQualifiedCallExpressionWithVariableInName()
        {
            Context context = new Context();
            Module module = new Module(null);
            context.SetValue("mod", module);
            context.SetValue("A", new Atom("add"));
            Function function = new Function(context, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            module.Context.SetValue("add/2", function);
            module.AddExportNames(new string[] { "add/2" });
            QualifiedCallExpression expr = new QualifiedCallExpression(new AtomExpression(new Atom("mod")), new VariableExpression(new Variable("A")), new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });

            Assert.IsTrue(expr.HasVariable());
            Assert.IsNotNull(expr.ModuleExpression);
            Assert.IsInstanceOfType(expr.ModuleExpression, typeof(AtomExpression));
            Assert.IsNotNull(expr.NameExpression);
            Assert.IsInstanceOfType(expr.NameExpression, typeof(VariableExpression));
            Assert.IsNotNull(expr.ArgumentExpressions);
            Assert.AreEqual(2, expr.ArgumentExpressions.Count);

            Assert.AreEqual(3, expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateQualifiedCallExpressionWithVariableInArguments()
        {
            Context context = new Context();
            Module module = new Module(null);
            context.SetValue("mod", module);
            context.SetValue("One", 1);
            Function function = new Function(context, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            module.Context.SetValue("add/2", function);
            module.AddExportNames(new string[] { "add/2" });
            QualifiedCallExpression expr = new QualifiedCallExpression(new AtomExpression(new Atom("mod")), new AtomExpression(new Atom("add")), new IExpression[] { new VariableExpression(new Variable("One")), new ConstantExpression(2) });

            Assert.IsTrue(expr.HasVariable());
            Assert.IsNotNull(expr.ModuleExpression);
            Assert.IsInstanceOfType(expr.ModuleExpression, typeof(AtomExpression));
            Assert.IsNotNull(expr.NameExpression);
            Assert.IsInstanceOfType(expr.NameExpression, typeof(AtomExpression));
            Assert.IsNotNull(expr.ArgumentExpressions);
            Assert.AreEqual(2, expr.ArgumentExpressions.Count);

            Assert.AreEqual(3, expr.Evaluate(context));
        }

        [TestMethod]
        public void RaiseIfNoFunctionExported()
        {
            Context context = new Context();
            Module module = new Module(null);
            context.SetValue("mod", module);
            Function function = new Function(context, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            module.Context.SetValue("add/2", function);
            QualifiedCallExpression expr = new QualifiedCallExpression(new AtomExpression(new Atom("mod")), new AtomExpression(new Atom("add")), new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });

            try
            {
                expr.Evaluate(context);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("undefined function mod:add/2", ex.Message);
            }
        }
    }
}
