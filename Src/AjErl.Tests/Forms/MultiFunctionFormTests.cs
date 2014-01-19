namespace AjErl.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Forms;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultiFunctionFormTests
    {
        [TestMethod]
        public void EvaluateWithOneForm()
        {
            Context context = new Context();
            FunctionForm form = this.MakeForm("f(0) -> 1.");
            MultiFunctionForm mform = new MultiFunctionForm(new FunctionForm[] { form });

            var result = mform.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MultiFunction));

            var mfunc = (MultiFunction)result;

            Assert.IsNotNull(mfunc.Functions);
            Assert.AreEqual(1, mfunc.Functions.Count);

            var defined = context.GetValue("f/1");

            Assert.IsNotNull(defined);
            Assert.IsInstanceOfType(defined, typeof(MultiFunction));
            Assert.AreSame(mfunc, defined);
        }

        [TestMethod]
        public void EvaluateWithThreeForm()
        {
            Context context = new Context();
            FunctionForm form1 = this.MakeForm("f(0) -> 1.");
            FunctionForm form2 = this.MakeForm("f(1) -> 1.");
            FunctionForm form3 = this.MakeForm("f(X) -> f(X-1) + f(X-2).");
            MultiFunctionForm mform = new MultiFunctionForm(new FunctionForm[] { form1, form2, form3 });

            var result = mform.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MultiFunction));

            var mfunc = (MultiFunction)result;

            Assert.IsNotNull(mfunc.Functions);
            Assert.AreEqual(3, mfunc.Functions.Count);

            var defined = context.GetValue("f/1");

            Assert.IsNotNull(defined);
            Assert.IsInstanceOfType(defined, typeof(MultiFunction));
            Assert.AreSame(mfunc, defined);
        }

        [TestMethod]
        public void RaiseIfArityIsWrong()
        {
            Context context = new Context();
            FunctionForm form1 = this.MakeForm("f(0) -> 1.");
            FunctionForm form2 = this.MakeForm("f(1) -> 1.");
            FunctionForm form3 = this.MakeForm("f(X, Y) -> f(X-1) + f(X-2).");
            MultiFunctionForm mform = new MultiFunctionForm(new FunctionForm[] { form1, form2, form3 });

            try
            {
                mform.Evaluate(context);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("head mismatch", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIfNameIsWrong()
        {
            Context context = new Context();
            FunctionForm form1 = this.MakeForm("f(0) -> 1.");
            FunctionForm form2 = this.MakeForm("g(1) -> 1.");
            FunctionForm form3 = this.MakeForm("f(X) -> f(X-1) + f(X-2).");
            MultiFunctionForm mform = new MultiFunctionForm(new FunctionForm[] { form1, form2, form3 });

            try
            {
                mform.Evaluate(context);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("head mismatch", ex.Message);
            }
        }

        private FunctionForm MakeForm(string text)
        {
            Parser parser = new Parser(text);
            return (FunctionForm)parser.ParseForm();
        }
    }
}
