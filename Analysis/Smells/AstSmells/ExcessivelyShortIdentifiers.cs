using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class ExcessivelyShortIdentifiers : AstSmell
    {
        public override string SmellName => "EXCESSIVELY_SHORT_ID";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is Identifier IdentifierNode)
                if (IdentifierNode.Name.Length < 3)
                    RegisterOccurrence(IdentifierNode.Location);

            foreach (var child in node.ChildNodes) AnalyzeNode(child, depth + 1);
        }
    }
}
