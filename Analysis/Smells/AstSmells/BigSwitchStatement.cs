using Esprima.Ast;

namespace BSDetector
{
    public class BigSwitchStatement : AstSmell
    {
        public override string SmellName => "BIG_SWITCH_STATEMENT";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is SwitchStatement switchStatementnNode && switchStatementnNode.Cases.Count > 10)
            {
                RegisterOccurrence(node.Location);                    
            }                
        }
    }
}
