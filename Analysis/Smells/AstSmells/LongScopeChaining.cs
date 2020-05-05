using Esprima;
using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class LongScopeChaining : AstSmell
    {
        public override string SmellName => "LONG_SCOPE_CHAINING";

        public override void AnalyzeNode(INode node, int levelOfFunction)
        {
            if (node is FunctionDeclaration FunctionDeclarationNode)
            {
                levelOfFunction += 1;
                foreach (var child in node.ChildNodes)
                    AnalyzeNode(child, levelOfFunction, FunctionDeclarationNode.Location);
            }
            else
            {
                foreach (var child in node.ChildNodes) AnalyzeNode(child, levelOfFunction);
            }
        }

        public override void AnalyzeNode(INode node, int levelOfFunction, Location location)
        {
            if (node is FunctionDeclaration FunctionDeclarationNode)
            {
                levelOfFunction += 1;
                if (levelOfFunction == 4) RegisterOccurrence(location);
            }

            foreach (var child in node.ChildNodes) AnalyzeNode(child, levelOfFunction, location);
        }
    }
}
