using Esprima.Ast;

namespace BSDetector {
    public class TooManyParametersArrowFunction : AstSmell {
        public TooManyParametersArrowFunction() {
            SmellName = "Too many parameters for arrow function";
            SmellDescription = "Maximum recommended number of parameters for an arrow function is: 4.";
        }
        public override void AnalyzeNode(INode node, int depth) {
            if (node is ArrowFunctionExpression) {
                var ArrowFunctionNode = (ArrowFunctionExpression)node;
                if (ArrowFunctionNode.Params.Count > 4) {
                    RegisterOccurance(ArrowFunctionNode.Location.Start.Line, ArrowFunctionNode.Location.Start.Column, ArrowFunctionNode.Location.End.Line, ArrowFunctionNode.Location.End.Column);
                }
            }
        }
    }
}