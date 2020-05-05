using Esprima.Ast;

namespace BSDetector
{
    public class TooManyParametersFunction : AstSmell
    {
        public override string SmellName
        {
            get
            {
                return "TOO_MANY_PARAMS_FUNCTION";
            }
        }

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is FunctionDeclaration RegularFunctionNode)
                if (RegularFunctionNode.Params.Count > 5)
                    RegisterOccurrence(RegularFunctionNode.Params.AsNodes());

            foreach (var child in node.ChildNodes) AnalyzeNode(child, depth + 1);
        }
    }
}