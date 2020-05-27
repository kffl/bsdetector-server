using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class SmallSwitchStatement : AstSmell
    {
        public override string SmellName => "SMALL_SWITCH_STATEMENT";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is SwitchStatement SwitchStatementNode && SwitchStatementNode.Cases.Count < 4)
                RegisterOccurrence(SwitchStatementNode.Location);
        }
    }
}