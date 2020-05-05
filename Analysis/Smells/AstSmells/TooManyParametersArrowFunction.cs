using Esprima.Ast;

namespace BSDetector
{
    public class TooManyParametersArrowFunction : AstSmell
    {
        public override string SmellName => "TOO_MANY_PARAMS_ARROW";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is ArrowFunctionExpression ArrowFunctionNode)
                if (ArrowFunctionNode.Params.Count > 4)
                    RegisterOccurrence(ArrowFunctionNode.Params.AsNodes());

            foreach (var child in node.ChildNodes) AnalyzeNode(child, depth + 1);
        }
    }
}
