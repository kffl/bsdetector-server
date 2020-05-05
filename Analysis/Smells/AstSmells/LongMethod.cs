using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class LongMethod : AstSmell
    {
        public override string SmellName => "LONG_METHOD";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is FunctionDeclaration FunctionDeclarationNode)
            {
                var location = FunctionDeclarationNode.Location;
                if (location.End.Line - location.Start.Line > 4)
                    RegisterOccurrence(FunctionDeclarationNode.Params.AsNodes());
            }

            foreach (var child in node.ChildNodes) AnalyzeNode(child, depth + 1);
        }
    }
}
