using System;
using Esprima;
using Esprima.Utils;
using Esprima.Ast;
using System.Collections.Generic;

namespace BSDetector {
    public class CodeAnalyzer {
        private string Code;
        private int linesAnalyzed = 0;
        private List<AstSmell> AstSmells = new List<AstSmell>{new TooManyParametersFunction(), new TooManyParametersArrowFunction()};
        private List<LineSmell> LineSmells = new List<LineSmell>{new LineTooLong()};

        public CodeAnalyzer(string Code) {
            this.Code = Code;
        }


        //simplified analysis is performed by iterating over the entire code line by line
        private void SimplifiedAnalysis() {

            string[] lines = Code.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);
            
            int lineNum = 0;
            string previousLine = "";
            foreach (var line in lines)
            {
                foreach (var smell in LineSmells)
                {
                    smell.AnalyzeLine(line, previousLine, lineNum);
                }
                lineNum++;
                previousLine = line;
            }

            linesAnalyzed = lineNum;
        }

        private void ASTreeDFS(INode node, int depth) {

            foreach (var smell in AstSmells)       
            {
                smell.AnalyzeNode(node, depth);
            }  

            foreach (var child in node.ChildNodes)
            {
                ASTreeDFS(child, depth+1);
            }
        }

        private Script BuildAST()
        {
            var customParserOptions = new ParserOptions();
            customParserOptions.Loc = true;
            customParserOptions.Range = true;

            var parser = new JavaScriptParser(Code, customParserOptions);
            var program = parser.ParseScript();
            
            return program;
        }

        //ast analysis is performed by going through the entire abstract syntax tree
        //...and analysing visited nodes
        private void ASTAnalysis() {
            
            var tree = BuildAST().Body;

            foreach (var node in tree.AsNodes())
            {
                ASTreeDFS(node, 0);
            }
        }

        public FileAnalysisResult AnalyzeCode()
        {
            SimplifiedAnalysis();
            ASTAnalysis();
            var smellsList = new List<Smell> (LineSmells);
            smellsList.AddRange(AstSmells);
            return new FileAnalysisResult {
                LinesAnalyzed=linesAnalyzed,
                SmellsDetected = smellsList.ToArray()
            };
        }

        public string GetASTasJSONstring()
        {
            var program = BuildAST();
            return program.ToJsonString(new AstJson
                                            .Options()
                                            .WithIncludingLineColumn(true));
        }
    }
}