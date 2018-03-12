using KbgSoft.LineCounter;
using Xunit;

namespace LineCounter.Tests
{
	public class TrimStringLensTest
	{
		[Fact]
		public void Length()
		{
			Assert.Equal(0, new TrimStringLens("    ").Length);
			Assert.Equal(6, new TrimStringLens("kasper").Length);
			Assert.Equal(6, new TrimStringLens("kasper ").Length);
			Assert.Equal(6, new TrimStringLens(" kasper ").Length);
			Assert.Equal(6, new TrimStringLens(" \tkasper \t").Length);
			Assert.Equal(6, new TrimStringLens(" \rkasper \n").Length);
		}

		[Fact]
		public void Startswith()
		{
			Assert.True(new TrimStringLens("kasper").StartsWithOrdinal("k"));
			Assert.True(new TrimStringLens("kasper").StartsWithOrdinal("ka"));
			Assert.True(new TrimStringLens("kasper").StartsWithOrdinal("kas"));
			Assert.True(new TrimStringLens("kasper").StartsWithOrdinal("kasp"));
			Assert.True(new TrimStringLens("kasper").StartsWithOrdinal("kaspe"));
			Assert.True(new TrimStringLens("kasper").StartsWithOrdinal("kasper"));

			Assert.False(new TrimStringLens("kasper").StartsWithOrdinal("kasperNOT"));
			Assert.False(new TrimStringLens("kasper").StartsWithOrdinal("ki"));
		}

		[Fact]
		public void OperatorAreEqual()
		{
			Assert.True(new TrimStringLens("{") == "{");
			Assert.True(new TrimStringLens(" {") == "{");
			Assert.True(new TrimStringLens(" { ") == "{");
		}
	}
}
