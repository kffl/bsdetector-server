using Esprima.Ast;
using System.Collections.Generic;
using System.Linq;

namespace BSDetector
{
    public class VariableNotDeclared : AstSmell
    {
        public override string SmellName => "VARIABLE_NOT_DECLARED";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is AssignmentExpression || node is UpdateExpression)
            {
                Identifier identifier = null;
                if (node is AssignmentExpression assignmentExpression)
                    identifier = assignmentExpression.ChildNodes.OfType<Identifier>().FirstOrDefault();
                else if (node is UpdateExpression updateExpression)
                {
                    identifier = updateExpression.ChildNodes.OfType<Identifier>().FirstOrDefault();
                }

                if (identifier != null)
                {
                    var names = new HashSet<string>(Scopes.scopes.SelectMany(i => i));
                    if (!names.Contains(identifier.Name))
                    {
                        RegisterOccurrence(identifier.Location);
                    }
                }
            }
        }
    }
}
