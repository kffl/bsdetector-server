using NUnit.Framework;
using BSDetector.Analysis.Smells.AstSmells;

namespace BSDetector.Tests.Smells
{
    [TestFixture]
    public class SmallSwitchStatementTest
    {
        private SmallSwitchStatement smell;
        private ProcessingAstSmell detector;

        [SetUp]
        public void Setup()
        {
            smell = new SmallSwitchStatement();
            detector = new ProcessingAstSmell(smell);
        }

        [Test]
        public void SmallSwitchStatementTestPos()
        {
            detector.code = @"const expr = 'Papayas';
                switch (expr) {
                    case 'Oranges':
                        console.log('Oranges are $0.59 a pound.');
                        break;
                    case 'Apples':
                        console.log('Apples are $0.31 a pound.');
                        break;
                    default:
                        console.log(`Sorry, we are out of ${expr}.`);
                }
            ";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(1));
        }

        [Test]
        public void SmallSwitchStatementTestNeg()
        {
            detector.code = @"const expr = 'Papayas';
                switch (expr) {
                    case 'Oranges':
                        console.log('Oranges are $0.59 a pound.');
                        break;
                    case 'Apples':
                        console.log('Apples are $0.31 a pound.');
                        break;
                    case 'Bananas':
                        console.log('Bananas are $0.48 a pound.');
                        break;
                    default:
                        console.log(`Sorry, we are out of ${expr}.`);
                }
            ";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(0));
        }
    }
}