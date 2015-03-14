using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamBinary.LineCounter;

namespace TeamBinary.LineCounter.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DirWalker()
        {
            var res = new DirWalker().DoWork(@"C:\Users\kbg\Documents\GitHub\StatePrinter\");
            Assert.AreEqual(2840, res.CodeLines);
            Assert.AreEqual(1184, res.DocumentationLines);
            Console.WriteLine(new WebFormatter().CreateGithubShields(res));
        }

        [TestMethod]
        public void Webformatter()
        {
            var stat = new Statistics() { CodeLines = 2399, DocumentationLines = 299 };
            var res = new WebFormatter().CreateGithubShields(stat);
            Console.WriteLine(  res);
            Assert.AreEqual(@"[![Stats](https://img.shields.io/badge/Code_lines-2,4_K-ff69b4.svg)]()
[![Stats](https://img.shields.io/badge/Doc_lines-299-ff69b4.svg)]()", res);
        }
    }
}
