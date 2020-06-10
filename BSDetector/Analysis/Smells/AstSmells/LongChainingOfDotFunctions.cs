using System.Collections.Generic;
using Esprima;
using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class LongChainingOfDotFunctions : AstSmell
    {
        private int Counter;

        private Location? loc;

        public override string SmellName => "CHAIN_DOT_FUNC";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is ExpressionStatement || node is CallExpression || node is BinaryExpression ||
                node is VariableDeclaration)
            {
                if (loc == null) loc = node.Location;
            }

            if (node is MemberExpression)
            {
                Counter++;
            }

            if (node is Identifier )
            {
                if (Counter > 5)
                {
                    RegisterOccurrence(loc.GetValueOrDefault());
                }
                loc = null;
                Counter = 0;
            }
        }
    }
}