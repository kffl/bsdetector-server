using NUnit.Framework;

namespace BSDetector.Tests.Smells
{
    public class LineTooLongTest
    {
        private LineTooLong detector;
        [SetUp]
        public void Setup()
        {
            detector = new LineTooLong();
        }

        [Test]
        public void LineLengthDetectionTestPos()
        {
            detector.AnalyzeLine("var k = `Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque sapien velit, aliquet eget commodo nec, auctor a sapien. Nam eu neque vulputate diam rhoncus faucibus. Curabitur quis varius libero. Lorem.`", "var l = 10;", 2);
            Assert.That(detector.GetOccurrences().Length, Is.EqualTo(1));
        }

        [Test]
        public void LineLengthDetectionTestNeg()
        {
            detector.AnalyzeLine("var l = 10;", "", 1);
            Assert.That(detector.GetOccurrences().Length, Is.EqualTo(0));
        }
    }
}