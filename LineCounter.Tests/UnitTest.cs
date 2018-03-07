using System;
using System.Diagnostics;
using NUnit.Framework;

namespace TeamBinary.LineCounter.Tests
{
    public class UnitTest
    {
		/*
		string lens						964			5.024630542
		ordinal startswith				1015		18.99441341
		ordnial rather than ==			1253		1.338582677
		string.length check				1270		0
		master uden length check		1270		0
		*/
		[Test]
		public void run()
		{
			var files = new DirWalker().GetFiles(@"C:\src\");
			Console.WriteLine("number files*: " + files.Length);
			Stopwatch w = Stopwatch.StartNew();
			var res = new DirWalker().DoWork(files);
			Console.WriteLine("Time: " + w.ElapsedMilliseconds);
			Console.WriteLine(new WebFormatter().CreateGithubShields(res));
		}

		[Test]
        public void DirWalker2()
        {
            var res = new DirWalker().DoWork(@"C:\src\Linecounter.net\");
            Console.WriteLine(new WebFormatter().CreateGithubShields(res));
            Assert.AreEqual(309, res.CodeLines);
            Assert.AreEqual(1548, res.DocumentationLines);
            Console.WriteLine(new WebFormatter().CreateGithubShields(res));
        }

		[Test]
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
