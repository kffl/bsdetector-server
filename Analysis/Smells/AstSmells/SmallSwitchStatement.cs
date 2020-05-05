using Esprima.Ast;

namespace BSDetector.Analysis.Smells.AstSmells
{
    public class SmallSwitchStatement : AstSmell
    {
        public SmallSwitchStatement()
        {
            SmellName = "Not enough case clauses in switch statement";
            SmellDescription = "Minimum recommended number of case clauses in switch statement is: 3.";
        }

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is SwitchStatement SwitchStatementnNode)
                if (SwitchStatementnNode.Cases.Count < 4)
                    RegisterOccurrence(SwitchStatementnNode.Location);

            foreach (var child in node.ChildNodes) AnalyzeNode(child, depth + 1);
        }
    }
}