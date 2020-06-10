using NUnit.Framework;
using BSDetector.Analysis.Smells.AstSmells;

namespace BSDetector.Tests.Smells
{
    [TestFixture]
    public class LongChainingOfDotFunctionsTest
    {
        private LongChainingOfDotFunctions smell;
        private ProcessingAstSmell detector;

        [SetUp]
        public void Setup()
        {
            smell = new LongChainingOfDotFunctions();
            detector = new ProcessingAstSmell(smell);
        }

        [Test]
        public void ExcessivelyShortIdentifiersTestPos()
        {
            detector.code = "x.add(3).subtract(2).multiply(6).divide(5).add(1).print();";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(1));
        }

        [Test]
        public void ExcessivelyShortIdentifiersTestNeg()
        {
            detector.code = "x.add(3).subtract(2).multiply(6).divide(5).print();";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(0));
        }
    }
}
