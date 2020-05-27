using Esprima.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSDetector
{
    public class NoDefaultCase : AstSmell
    {
        public override string SmellName => "NO_DEFAULT_CASE";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is SwitchStatement switchStatement)
            {
                var switchCases = switchStatement.ChildNodes.OfType<SwitchCase>();
                if (switchCases.All(switchCase => switchCase.Test != null))
                {
                    RegisterOccurrence(node.Location);
                }
            }
        }
    }
}
