using Esprima.Ast;

namespace BSDetector
{
    public class TooManyParametersFunction : AstSmell
    {
        public TooManyParametersFunction()
        {
            SmellName = "Too many parameters for a function declaration";
            SmellDescription = "Maximum recommended number of parameters for a regular function is: 5.";
        }
        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is FunctionDeclaration RegularFunctionNode)
            {
                if (RegularFunctionNode.Params.Count > 5)
                {
                    RegisterOccurence(RegularFunctionNode.Location);
                }
            }
        }
    }
}