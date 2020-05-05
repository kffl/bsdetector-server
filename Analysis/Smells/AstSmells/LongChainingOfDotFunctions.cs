using Esprima;
using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class LongChainingOfDotFunctions : AstSmell
    {
        public override string SmellName => "CHAIN_DOT_FUNC";

        public override void AnalyzeNode(INode node, int chainLength)
        {
            if (node is MemberExpression MemberNode)
            {
                chainLength += 1;
                foreach (var child in node.ChildNodes) AnalyzeNode(child, chainLength, MemberNode.Location);
            }
            else
            {
                foreach (var child in node.ChildNodes) AnalyzeNode(child, chainLength);
            }
        }

        public override void AnalyzeNode(INode node, int chainLength, Location location)
        {
            if (node is MemberExpression MemberNode)
            {
                chainLength += 1;
                if (chainLength == 6) RegisterOccurrence(location);
            }

            foreach (var child in node.ChildNodes) AnalyzeNode(child, chainLength, location);
        }
    }
}
