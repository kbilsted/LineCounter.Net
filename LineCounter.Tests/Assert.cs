using NUnit.Framework;


namespace LineCounter.Tests
{
    public static class Assert
    {
        public static void AreEqual(object expected, object actual) => NUnit.Framework.Assert.That(actual, Is.EqualTo(expected));
        public static void True(bool actual) => NUnit.Framework.Assert.That(actual, Is.EqualTo(true));
        public static void False(bool actual) => NUnit.Framework.Assert.That(actual, Is.EqualTo(false));
    }
}
