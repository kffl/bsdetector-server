using Esprima.Ast;
using System.Collections.Generic;
using System.Linq;

namespace BSDetector
{
    public class DuplicatedIdentifier : AstSmell
    {
        public override string SmellName => "DUPLICATED_IDENTIFIER";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is FunctionDeclaration NormalF)
            {
                var names = new HashSet<string>(Scopes.scopes.SelectMany(i => i));

                var funIdentifier = NormalF.ChildNodes.OfType<Identifier>().First();
                if (names.Contains(funIdentifier.Name))
                {
                    RegisterOccurrence(funIdentifier.Location);
                }
                Scopes.scopes[^1].Add(funIdentifier.Name);

                foreach (var param in NormalF.Params.OfType<Identifier>())
                {
                    if (names.Contains(param.Name))
                    {
                        RegisterOccurrence(param.Location);
                    }
                    Scopes.scopes[^1].Add(param.Name);
                }
            }
            if (node is VariableDeclarator variableDeclarator)
            {
                var names = new HashSet<string>(Scopes.scopes.SelectMany(i => i)); 
                
                string name = variableDeclarator.ChildNodes.OfType<Identifier>().First().Name;
                if (names.Contains(name))
                {
                    RegisterOccurrence(node.Location);
                }
                Scopes.scopes[^1].Add(name);
            }
        }
    }
}
