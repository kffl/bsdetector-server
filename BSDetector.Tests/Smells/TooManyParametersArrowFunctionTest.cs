using NUnit.Framework;
using BSDetector.Analysis.Smells.AstSmells;

namespace BSDetector.Tests.Smells
{
    [TestFixture]
    public class TooManyParametersArrowFunctionTest
    {
        private TooManyParametersArrowFunction smell;
        private ProcessingAstSmell detector;

        [SetUp]
        public void Setup()
        {
            smell = new TooManyParametersArrowFunction();
            detector = new ProcessingAstSmell(smell);
        }

        [Test]
        public void TooManyParametersArrowFunctionTestPos()
        {
            detector.code = "(par1, par2, par3, par4, par5) => { return 2; }";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(1));
        }

        [Test]
        public void TooManyParametersArrowFunctionTestNeg()
        {
            detector.code = "(par1, par2, par3, par4) => { return 2; }";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(0));
        }
    }
}