using KbgSoft.LineCounter;
using NUnit.Framework;

namespace LineCounter.Tests
{
	public class TrimStringLensTest
	{
		 [Test]
		public void Length()
		{
			Assert.AreEqual(0, new TrimStringLens("    ").Length);
			Assert.AreEqual(6, new TrimStringLens("kasper").Length);
			Assert.AreEqual(6, new TrimStringLens("kasper ").Length);
			Assert.AreEqual(6, new TrimStringLens(" kasper ").Length);
			Assert.AreEqual(6, new TrimStringLens(" \tkasper \t").Length);
			Assert.AreEqual(6, new TrimStringLens(" \rkasper \n").Length);
		}

		 [Test]
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

		 [Test]
		public void OperatorAreEqual()
		{
			Assert.True(new TrimStringLens("{") == "{");
			Assert.True(new TrimStringLens(" {") == "{");
			Assert.True(new TrimStringLens(" { ") == "{");
		}
	}
}
