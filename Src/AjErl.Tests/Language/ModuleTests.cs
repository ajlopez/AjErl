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
            Assert.AreSame(module, module.Context.Module);
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

        [TestMethod]
        public void AddExportNames()
        {
            Module module = new Module(null);

            Assert.IsNotNull(module.ExportNames);
            Assert.AreEqual(0, module.ExportNames.Count);

            module.AddExportNames(new string[] { "foo/1", "foo/2" });

            Assert.AreEqual(2, module.ExportNames.Count);
            Assert.IsTrue(module.ExportNames.Contains("foo/1"));
            Assert.IsTrue(module.ExportNames.Contains("foo/2"));

            module.AddExportNames(new string[] { "foo/2", "bar/1" });

            Assert.AreEqual(3, module.ExportNames.Count);
            Assert.IsTrue(module.ExportNames.Contains("foo/1"));
            Assert.IsTrue(module.ExportNames.Contains("foo/2"));
            Assert.IsTrue(module.ExportNames.Contains("bar/1"));
        }
    }
}
