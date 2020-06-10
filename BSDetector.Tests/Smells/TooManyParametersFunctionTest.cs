using NUnit.Framework;
using BSDetector.Analysis.Smells.AstSmells;

namespace BSDetector.Tests.Smells
{
    [TestFixture]
    public class TooManyParametersFunctionTest
    {
        private TooManyParametersFunction smell;
        private ProcessingAstSmell detector;

        [SetUp]
        public void Setup()
        {
            smell = new TooManyParametersFunction();
            detector = new ProcessingAstSmell(smell);
        }

        [Test]
        public void TooManyParametersFunctionTestPos()
        {
            detector.code = "function name(par1, par2, par3, par4, par5, par6) { return 2; }";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(1));
        }

        [Test]
        public void TooManyParametersFunctionTestNeg()
        {
            detector.code = "function name(par1, par2, par3, par4, par5) { return 2; }";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(0));
        }
    }
}