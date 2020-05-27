using Esprima.Ast;

namespace BSDetector
{
    public class NonExplicitOctal : AstSmell
    {
        public override string SmellName => "NON_EXPLICIT_OCTAL";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is Literal)
            {
                string raw = ((Literal)node).Raw;
                if (
                    raw.StartsWith('0') &&
                    !raw.Equals('0') &&
                    !raw.StartsWith("0o")) // isn't explicitly set to octal
                {
                    RegisterOccurrence(node.Location);
                }
            }
        }
    }
}
