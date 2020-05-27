using Esprima.Ast;
using System.Linq;

namespace BSDetector
{
    public class EmptyStatementSmell : AstSmell
    {
        public override string SmellName => "EMPTY_STATEMENT";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (
                node is EmptyStatement ||
                (node is BlockStatement blockStatement && !blockStatement.ChildNodes.Any())
                )
            {
                    RegisterOccurrence(node.Location);
            }
        }
    }
}
