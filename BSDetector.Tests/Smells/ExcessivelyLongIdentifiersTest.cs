using NUnit.Framework;
using BSDetector.Analysis.Smells.AstSmells;

namespace BSDetector.Tests.Smells
{
    [TestFixture]
    public class ExcessivelyLongIdentifiersTest
    {
        private ExcessivelyLongIdentifiers smell;
        private ProcessingAstSmell detector;

        [SetUp]
        public void Setup()
        {
            smell = new ExcessivelyLongIdentifiers();
            detector = new ProcessingAstSmell(smell);
        }

        [Test]
        public void ExcessivelyLongIdentifiersTestPos()
        {
            detector.code = "var red_long_sleeve_basketballShirt  = 49.50;";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(1));
        }

        [Test]
        public void ExcessivelyLongIdentifiersTestNeg()
        {
            detector.code = "var redLongSleeveBasketballShirt30 = 49.50;";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(0));
        }
    }
}