using System;
using System.Collections.Generic;
using System.Text;
using Esprima;
using Esprima.Ast;

namespace BSDetector.Tests.Smells
{
    class ProcessingAstSmell
    {
        public string code { get; set; }
        private AstSmell smell;

        public ProcessingAstSmell(AstSmell smell)
        {
            this.smell = smell;
        }

        public void ASTAnalysis()
        {
            var tree = BuildAST().Body;
            foreach (var node in tree.AsNodes()) ASTreeDFS(node, 0);
        }

        private Script BuildAST()
        {
            var customParserOptions = new ParserOptions();
            customParserOptions.Loc = true;
            customParserOptions.Range = true;
            var parser = new JavaScriptParser(code, customParserOptions);
            var program = parser.ParseScript();
            return program;
        }

        private void ASTreeDFS(INode node, int depth)
        {
            smell.AnalyzeNode(node, depth);
            foreach (var child in node.ChildNodes)
            {
                ASTreeDFS(child, depth + 1);
            }
        }
    }
}
