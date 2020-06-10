using NUnit.Framework;
using BSDetector.Analysis.Smells.AstSmells;

namespace BSDetector.Tests.Smells
{
    [TestFixture]
    public class LongScopeChainingTest
    {
        private LongScopeChaining smell;
        private ProcessingAstSmell detector;

        [SetUp]
        public void Setup()
        {
            smell = new LongScopeChaining();
            detector = new ProcessingAstSmell(smell);
        }

        [Test]
        public void ExcessivelyShortIdentifiersTestPos()
        {
            detector.code = @"function start(x) {
                var tmp = 20;
                    function foo(y)
                    {
                        ++tmp;
                        function mid(z)
                        {
                            tmp -= 10;
                            function end(q)
                            {
                                document.write(x + y + z + q + tmp);
                            }
                            end(1);
                        }
                        mid(3);
                    }
                    foo(10);
                }
                start(2);
            ";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(1));
        }

        [Test]
        public void ExcessivelyShortIdentifiersTestNeg()
        {
            detector.code = @"function start(x) {
                var tmp = 20;
                    function foo(y)
                    {
                        ++tmp;
                        function end(q)
                        {
                            document.write(x + y + q + tmp);
                        }
                        end(1);
                    }
                    foo(10);
                }
                start(2);
            ";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(0));
        }
    }
}