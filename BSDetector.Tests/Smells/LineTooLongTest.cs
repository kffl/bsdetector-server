using System.Runtime.CompilerServices;
using NUnit.Framework;
using BSDetector;

namespace BSDetector.Tests
{
    public class Tests
    {
        private LineTooLong detector;
        [SetUp]
        public void Setup()
        {
            detector = new LineTooLong();
        }

        [Test]
        public void LineLengthDetectionTest()
        {
            detector.AnalyzeLine("var l = 10;", "", 1);
            detector.AnalyzeLine("var k = `Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque sapien velit, aliquet eget commodo nec, auctor a sapien. Nam eu neque vulputate diam rhoncus faucibus. Curabitur quis varius libero. Lorem.`", "var l = 10;", 2);
            Assert.That(detector.GetOccurrences().Length, Is.EqualTo(1));
        }
    }
}