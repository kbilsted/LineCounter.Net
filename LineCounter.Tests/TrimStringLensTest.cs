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
		}

		[Test]
		public void Startswith()
		{
			Assert.AreEqual(true, new TrimStringLens("kasper").StartsWithOrdinal("k"));
			Assert.AreEqual(true, new TrimStringLens("kasper").StartsWithOrdinal("ka"));
			Assert.AreEqual(true, new TrimStringLens("kasper").StartsWithOrdinal("kas"));
			Assert.AreEqual(true, new TrimStringLens("kasper").StartsWithOrdinal("kasp"));
			Assert.AreEqual(true, new TrimStringLens("kasper").StartsWithOrdinal("kaspe"));
			Assert.AreEqual(true, new TrimStringLens("kasper").StartsWithOrdinal("kasper"));

			Assert.AreEqual(false, new TrimStringLens("kasper").StartsWithOrdinal("kasperNOT"));
			Assert.AreEqual(false, new TrimStringLens("kasper").StartsWithOrdinal("ki"));
		}

		[Test]
		public void OperatorAreEqual()
		{
			Assert.AreEqual(true, new TrimStringLens("{") == "{");
			Assert.AreEqual(true, new TrimStringLens(" {") == "{");
			Assert.AreEqual(true, new TrimStringLens(" { ") == "{");
		}
	}
}
