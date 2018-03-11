using Xunit;
using System;
using KbgSoft.LineCounter.Strategies;

namespace TeamBinary.LineCounter.Tests
{
    public class CsStrategyTests
    {
		[Fact]
        public void IgnoredLines() {
			var endOfObjectInitializer = "};";

			var codeLines = new CSharpStrategy().Count(new []{"", " ; "," { ", " } ",endOfObjectInitializer}).CodeLines;

			Assert.Equal(0, codeLines);

			Console.WriteLine(new CSharpStrategy().Count(@"C:\Users\Kbg\Desktop\kbgit.cs").CodeLines);
			Console.WriteLine(new CSharpStrategy().Count(@"C:\Users\Kbg\Desktop\kbgit.cs").DocumentationLines);
		}

	    [Fact]
	    public void Manual()
	    {
		    Console.WriteLine(new CSharpStrategy().Count(@"C:\Users\Kbg\Desktop\kbgit.cs").CodeLines);
	    }
	}
}
