using Esprima.Ast;
using System;
using System.Collections.Generic;

namespace BSDetector
{
    public class ReservedWord : AstSmell
    {
        static HashSet<string> reservedWords = new HashSet<string>() {
            "await", "class", "const", "enum", "export", "extends", "implements", "import", "interface", "let", "package", "private", "protected", "public", "static", "super", "yield"
        };
        public override string SmellName => "RESERVED_WORD_USED";

        public override void AnalyzeNode(INode node, int depth)
        {
            if (node is Identifier identifier && reservedWords.Contains(identifier.Name))
            {
                RegisterOccurrence(node.Location);
            }
        }
    }
}
