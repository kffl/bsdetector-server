using Esprima.Ast;
using System.Linq;

namespace BSDetector
{
    public class DefaultCaseNotLast : AstSmell
    {
        public override string SmellName => "DEFAULT_CASE_NOT_LAST";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is SwitchStatement switchStatement)
            {
                var switchCases = switchStatement.ChildNodes.OfType<SwitchCase>();
                var defaultCase = switchCases.FirstOrDefault(switchCase => (Literal)switchCase.Test == null);
                if (
                    defaultCase != null &&              // there is default case
                    switchCases.Last().Test != null)    // but it's not the last case
                {
                    RegisterOccurrence(defaultCase.Location);
                }
            }
        }
    }
}
