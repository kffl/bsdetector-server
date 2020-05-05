using Esprima.Ast;

namespace BSDetector
{
    public class TooManyParametersArrowFunction : AstSmell
    {
        public override string SmellName
        {
            get
            {
                return "TOO_MANY_PARAMS_ARROW";
            }
        }

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is ArrowFunctionExpression ArrowFunctionNode)
            {
                if (ArrowFunctionNode.Params.Count > 4)
                {
                    RegisterOccurrence(ArrowFunctionNode.Params.AsNodes());
                }
            }
        }
    }
}