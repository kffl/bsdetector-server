using Esprima;
using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class LongChainingOfDotFunctions : AstSmell
    {
        public LongChainingOfDotFunctions()
        {
            SmellName = "Too long chaining of functions with the dot operator";
            SmellDescription = "Maximum recommended number of function for chain with dot operator is: 6.";
        }

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