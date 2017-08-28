using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineCounter.Tests
{
	[TestClass]
	public class TrimStringLensTest
	{
		[TestMethod]
		public void Length()
		{
			Assert.AreEqual(0, new TrimStringLens("    ").Length);
			Assert.AreEqual(6, new TrimStringLens("kasper").Length);
			Assert.AreEqual(6, new TrimStringLens("kasper ").Length);
			Assert.AreEqual(6, new TrimStringLens(" kasper ").Length);
		}

		[TestMethod]
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

		[TestMethod]
		public void OperatorEquals()
		{
			Assert.AreEqual(true, new TrimStringLens("{") == "{");
			Assert.AreEqual(true, new TrimStringLens(" {") == "{");
			Assert.AreEqual(true, new TrimStringLens(" { ") == "{");
		}
	}
}
