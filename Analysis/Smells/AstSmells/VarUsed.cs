using Esprima.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSDetector
{
    public class VarUsed : AstSmell
    {
        public override string SmellName => "VAR_USED";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is VariableDeclaration vd && vd.Kind.ToString().Equals("Var"))
            {
                RegisterOccurrence(node.Location);
            }
        }
    }
}
