using Esprima.Ast;
using System.Linq;

namespace BSDetector
{
    public class NoBreakInSwitchCase : AstSmell
    {
        public override string SmellName => "NO_BREAK_IN_SWITCHCASE";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is SwitchCase switchCase && 
                switchCase.Test != null &&                              //case is not "default"
                !switchCase.ChildNodes.OfType<BreakStatement>().Any()   //doesn't have break statements
            )
            {
                RegisterOccurrence(node.Location);
            }
        }
    }
}
