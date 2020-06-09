using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class LongMethod : AstSmell
    {
        public override string SmellName => "LONG_METHOD";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is FunctionDeclaration FunctionDeclarationNode && FunctionDeclarationNode.Location.End.Line -
                FunctionDeclarationNode.Location.Start.Line > 40)
                RegisterOccurrence(FunctionDeclarationNode.Location);
        }
    }
}