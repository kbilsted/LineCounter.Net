using NUnit.Framework;

namespace TeamBinary.LineCounter.Tests
{
    public class CsStrategyTests
    {
		[Test]
        public void IgnoredLines() {
			var endOfObjectInitializer = "};";

			var codeLines = new CSharpStrategy().Count(new []{"", " ; "," { ", " } ",endOfObjectInitializer}).CodeLines;

			Assert.AreEqual(0, codeLines);
		}
    }
}
