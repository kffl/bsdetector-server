using Esprima.Ast;

namespace BSDetector {
    public class TooManyParametersFunction : AstSmell {
        public TooManyParametersFunction() {
            SmellName = "Too many parameters for a function declaration";
            SmellDescription = "Maximum recommended number of parameters for a regular function is: 5.";
        }   
        public override void AnalyzeNode(INode node, int depth) {
            if (node is FunctionDeclaration) {
                var RegularFunctionNode = (FunctionDeclaration)node;
                if (RegularFunctionNode.Params.Count > 5) {
                    RegisterOccurance(RegularFunctionNode.Location.Start.Line, RegularFunctionNode.Location.Start.Column, RegularFunctionNode.Location.End.Line, RegularFunctionNode.Location.End.Column);
                }
            }
        }
    }
}