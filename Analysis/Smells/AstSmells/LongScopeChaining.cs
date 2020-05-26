using System.Collections.Generic;
using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class LongScopeChaining : AstSmell
    {
        public override string SmellName => "LONG_SCOPE_CHAINING";

        private Stack<int> LevelStack = new Stack<int>();


        public override void AnalyzeNode(INode node, int depth)
        {
            while (LevelStack.Count > 0 && LevelStack.Peek() >= depth)
                LevelStack.Pop();
            if (node is FunctionDeclaration FunctionDeclarationNode)
            {
                LevelStack.Push(depth);
                if (LevelStack.Count == 4) RegisterOccurrence(FunctionDeclarationNode.Location);
            }
        }
    }
}