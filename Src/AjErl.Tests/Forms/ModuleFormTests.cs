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
    public class ModuleFormTests
    {
        [TestMethod]
        public void EvaluateModuleForm()
        {
            ModuleForm form = new ModuleForm("mymodule");

            Assert.AreEqual("mymodule", form.Name);

            Module module = new Module(null);

            form.Evaluate(module.Context);

            Assert.AreEqual("mymodule", form.Name);
        }
    }
}
