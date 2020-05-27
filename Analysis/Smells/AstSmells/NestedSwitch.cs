using Esprima.Ast;
using System.Linq;

namespace BSDetector
{
    public class NestedSwitch : AstSmell
    {
        public override string SmellName => "NESTED_SWITCH";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is SwitchCase switchCase)
            {
                var nestedSwitch = switchCase.ChildNodes.OfType<SwitchStatement>().FirstOrDefault();
                if (nestedSwitch != null)
                {
                    RegisterOccurrence(nestedSwitch.Location);
                }                
            }
        }
    }
}
