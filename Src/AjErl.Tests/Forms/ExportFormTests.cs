namespace AjErl.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Forms;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExportFormTests
    {
        [TestMethod]
        public void EvaluateExportForm()
        {
            ExportForm form = new ExportForm(new string[] { "foo/1", "bar/2" });

            Assert.AreEqual(2, form.Names.Count);
            Assert.IsTrue(form.Names.Contains("foo/1"));
            Assert.IsTrue(form.Names.Contains("bar/2"));

            Module module = new Module(null);

            form.Evaluate(module.Context);

            Assert.AreEqual(2, module.ExportNames.Count);
            Assert.IsTrue(module.ExportNames.Contains("foo/1"));
            Assert.IsTrue(module.ExportNames.Contains("bar/2"));
        }
    }
}
