using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamBinary.LineCounter;

namespace TeamBinary.LineCounter.Tests
{
    [TestClass]
    public class UnitTest1
    {
		/*
		string lens						964			5.024630542
		ordinal startswith				1015		18.99441341
		ordnial rather than ==			1253		1.338582677
		string.length check				1270		0
		master uden length check		1270		0
		*/
		[TestMethod]
		public void run()
		{
			var files = DirWalker.GetFiles(@"C:\src\");
			Console.WriteLine("number files*: " + files.Length);
			Stopwatch w = Stopwatch.StartNew();
			var res = new DirWalker().DoWork(files);
			Console.WriteLine("Time: " + w.ElapsedMilliseconds);
			Console.WriteLine(new WebFormatter().CreateGithubShields(res));
		}

		[TestMethod]
        public void DirWalker2()
        {
            var res = new DirWalker().DoWork(@"C:\Users\kbg\Documents\GitHub\StatePrinter\");
            Console.WriteLine(new WebFormatter().CreateGithubShields(res));
            Assert.AreEqual(3257, res.CodeLines);
            Assert.AreEqual(1376, res.DocumentationLines);
            Console.WriteLine(new WebFormatter().CreateGithubShields(res));
        }

        [TestMethod]
        public void Webformatter()
        {
            var stat = new Statistics() { CodeLines = 2399, DocumentationLines = 299 };
            var res = new WebFormatter().CreateGithubShields(stat);
            Console.WriteLine(res);
            Assert.AreEqual(@"[![Stats](https://img.shields.io/badge/Code_lines-2,4_K-ff69b4.svg)]()
[![Stats](https://img.shields.io/badge/Doc_lines-299-ff69b4.svg)]()", res);
        }
    }
}
