using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class ExcessivelyLongIdentifiers : AstSmell
    {
        public override string SmellName => "EXCESSIVELY_LONG_ID";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is Identifier IdentifierNode && IdentifierNode.Name.Length > 30)
                RegisterOccurrence(IdentifierNode.Location);
        }
    }
}