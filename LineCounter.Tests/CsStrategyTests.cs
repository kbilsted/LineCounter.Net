using Xunit;

namespace TeamBinary.LineCounter.Tests
{
    public class CsStrategyTests
    {
		[Fact]
        public void IgnoredLines() {
			var endOfObjectInitializer = "};";

			var codeLines = new CSharpStrategy().Count(new []{"", " ; "," { ", " } ",endOfObjectInitializer}).CodeLines;

			Assert.Equal(0, codeLines);
		}
    }
}
