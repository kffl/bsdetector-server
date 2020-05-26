using System.Collections.Generic;
using Esprima;
using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class LongChainingOfDotFunctions : AstSmell
    {
        private readonly Queue<Location> LocationQueue = new Queue<Location>();

        private int Counter;

        private Position endPosition;

        private Position startPosition;

        public override string SmellName => "CHAIN_DOT_FUNC";


        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is ExpressionStatement || node is CallExpression || node is BinaryExpression ||
                node is VariableDeclaration) LocationQueue.Enqueue(node.Location);

            if (node is MemberExpression)
            {
                if (++Counter == 1) startPosition = node.Location.Start;
                endPosition = node.Location.End;
            }

            if (node is Identifier && LocationQueue.Count > 0 && Counter != 0)
            {
                if (Counter >= 5)
                    RegisterOccurrence(new Location(startPosition, endPosition, LocationQueue.Peek().Source));
                LocationQueue.Dequeue();
                Counter = 0;
            }
        }
    }
}