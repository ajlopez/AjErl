namespace AjErl.Tests.Language
{
    using System;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ModuleTests
    {
        [TestMethod]
        public void CreateModuleWithoutParentContext()
        {
            Module module = new Module(null);

            Assert.IsNotNull(module.Context);
            Assert.IsNull(module.Context.Parent);
        }

        [TestMethod]
        public void CreateModuleWithParentContext()
        {
            Context parent = new Context();
            Module module = new Module(parent);

            Assert.IsNotNull(module.Context);
            Assert.IsNotNull(module.Context.Parent);
            Assert.AreSame(parent, module.Context.Parent);
        }
    }
}
