namespace AjErl.Tests.Functions
{
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Functions;
    using AjErl.Language;

    [TestClass]
    public class CompileModuleFunctionTests
    {
        [TestMethod]
        [DeploymentItem("Modules\\arith.erl")]
        public void CompileArithModule()
        {
            Machine machine = new Machine();
            CompileModuleFunction func = new CompileModuleFunction(machine);

            var result = func.Apply(machine.RootContext, new object[] { new Atom("arith") });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Tuple));

            var tuple = (Tuple)result;

            Assert.AreEqual(2, tuple.Arity);

            var elem1 = tuple.ElementAt(0);

            Assert.IsNotNull(elem1);
            Assert.IsInstanceOfType(elem1, typeof(Atom));
            Assert.AreEqual("ok", ((Atom)elem1).Name);

            var elem2 = tuple.ElementAt(1);

            Assert.IsNotNull(elem2);
            Assert.IsInstanceOfType(elem2, typeof(Atom));
            Assert.AreEqual("arith", ((Atom)elem2).Name);

            var result2 = machine.RootContext.GetValue("arith");
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(Module));
            Assert.AreEqual("arith", ((Module)result2).Name);
        }
    }
}
