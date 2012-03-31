using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjErl.Compiler;

namespace AjErl.Tests.Compiler
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetAtom()
        {
            Lexer lexer = new Lexer("ok");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("ok", token.Value);
        }

        [TestMethod]
        public void GetAtomWithSurroundingSpaces()
        {
            Lexer lexer = new Lexer("  ok   ");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("ok", token.Value);
        }
    }
}
