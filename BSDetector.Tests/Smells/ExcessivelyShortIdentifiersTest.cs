using NUnit.Framework;
using BSDetector.Analysis.Smells.AstSmells;

namespace BSDetector.Tests.Smells
{
    [TestFixture]
    public class ExcessivelyShortIdentifiersTest
    {
        private ExcessivelyShortIdentifiers smell;
        private ProcessingAstSmell detector;

        [SetUp]
        public void Setup()
        {
            smell = new ExcessivelyShortIdentifiers();
            detector = new ProcessingAstSmell(smell);
        }

        [Test]
        public void ExcessivelyShortIdentifiersTestPos()
        {
            detector.code = "var xy = 1;";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(1));
        }

        [Test]
        public void ExcessivelyShortIdentifiersTestNeg()
        {
            detector.code = "var xyz = 1;";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(0));
        }
    }
}