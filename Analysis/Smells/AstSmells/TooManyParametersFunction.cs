using Esprima.Ast;

namespace BSDetector
{
    public class TooManyParametersFunction : AstSmell
    {
        public override string SmellName => "TOO_MANY_PARAMS_FUNCTION";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is FunctionDeclaration RegularFunctionNode && RegularFunctionNode.Params.Count > 5)
                RegisterOccurrence(RegularFunctionNode.Params.AsNodes());
        }
    }
}