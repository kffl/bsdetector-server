using Esprima.Ast;

namespace BSDetector
{
    public class GlobalThis : AstSmell
    {
        public override string SmellName => "GLOBAL_THIS_USED";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is ThisExpression && depth == 4)
            {
                RegisterOccurrence(node.Location);
            }
        }
    }
}
