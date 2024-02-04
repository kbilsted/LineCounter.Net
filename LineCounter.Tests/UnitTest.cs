using System;
using System.Diagnostics;
using KbgSoft.LineCounter;
using NUnit.Framework;

namespace TeamBinary.LineCounter.Tests
{
    [TestFixture]
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
        public void Run()
        {
            var files = new LineCounting().GetFiles(@"C:\src\");
            Stopwatch w = Stopwatch.StartNew();
            var res = new LineCounting().CountFiles(files);
            Console.WriteLine("Time: " + w.ElapsedMilliseconds);
            Console.WriteLine(new WebFormatter().CreateGithubShields(res.Total));
        }

        [Test]
        public void RunSrc()
        {
            var files = new LineCounting().GetFiles(@"C:\src\");
            Stopwatch w = Stopwatch.StartNew();
            var res = new LineCounting().CountFiles(files);
            Console.WriteLine("files: " + res.Files);
            Console.WriteLine("Time: " + w.ElapsedMilliseconds);
            Console.WriteLine(res.Print());
            Console.WriteLine($"total: code {res.Total.CodeLines} and test {res.Total.TestCodeLines} and doc {res.Total.DocumentationLines}");
        }


        [Test]
        public void Webformatter()
        {
            var stat = new Statistics() { CodeLines = 2399, DocumentationLines = 299 };
            var res = new WebFormatter().CreateGithubShields(stat);
            Console.WriteLine(res);
            Assert.AreEqual(@"[![Stats](https://img.shields.io/badge/Code_lines-2.4_K-ff69b4.svg)]()
[![Stats](https://img.shields.io/badge/Test_lines-0-69ffb4.svg)]()
[![Stats](https://img.shields.io/badge/Doc_lines-299-ffb469.svg)]()", res);
        }
    }
}
