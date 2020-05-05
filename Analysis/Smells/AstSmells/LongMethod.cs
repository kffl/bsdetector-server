using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class LongMethod : AstSmell
    {
        public LongMethod()
        {
            SmellName = "Too long method";
            SmellDescription = "Maximum recommended number of lines of code is: 40.";
        }

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