using Esprima.Ast;

namespace BSDetector
{
    public class TooManyParametersFunction : AstSmell
    {
        public override string SmellName
        {
            get
            {
                return "Maximum recommended number of parameters for a regular function is: 5.";
            }
        }

        public override string SmellDescription
        {
            get
            {
                return "Maximum recommended number of parameters for a regular function is: 5.";
            }
        }
        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is FunctionDeclaration RegularFunctionNode)
            {
                if (RegularFunctionNode.Params.Count > 5)
                {
                    RegisterOccurrence(RegularFunctionNode.Params.AsNodes());
                }
            }
        }
    }
}