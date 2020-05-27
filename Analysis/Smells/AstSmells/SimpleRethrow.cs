using Esprima.Ast;
using System;
using System.Linq;

namespace BSDetector
{
    public class SimpleRethrow : AstSmell
    {
        public override string SmellName => "SIMPLE_RETHROW";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is CatchClause catchClause)
            {
                var blockStatement = catchClause.ChildNodes.OfType<BlockStatement>().First();
                if (blockStatement.ChildNodes.FirstOrDefault() is ThrowStatement)
                {
                    RegisterOccurrence(node.Location);
                }
            }
        }
    }
}
