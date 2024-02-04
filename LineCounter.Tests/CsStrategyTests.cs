using KbgSoft.LineCounter.Strategies;
using NUnit.Framework;
using System;

namespace TeamBinary.LineCounter.Tests
{

    [TestFixture]
    public class CsStrategyTests
    {
        [Test]
        public void IgnoredLines()
        {
            var endOfObjectInitializer = "};";

            var codeLines = new CSharpStrategy().Count(new[] { " ; ", " { ", " } ", endOfObjectInitializer }).CodeLines;

            Assert.AreEqual(0, codeLines);

            //Console.WriteLine(new CSharpStrategy().Count(@"C:\src\KBGit\git.cs").CodeLines);
            //Console.WriteLine(new CSharpStrategy().Count(@"C:\src\KBGit\git.cs").DocumentationLines);
        }

        [Test]
        public void Manual()
        {
            Console.WriteLine(new CSharpStrategy().Count(@"C:\src\KBGit\git.cs").CodeLines);
        }
    }
}
