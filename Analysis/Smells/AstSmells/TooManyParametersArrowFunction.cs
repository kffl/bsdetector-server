using Esprima.Ast;

namespace BSDetector
{
    public class TooManyParametersArrowFunction : AstSmell
    {
        public override string SmellName
        {
            get
            {
                return "Too many parameters for arrow function";
            }
        }

        public override string SmellDescription
        {
            get
            {
                return "maximum recommended number of parameters for an arrow function is: 4.";
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