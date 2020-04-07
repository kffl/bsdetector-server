using System;
using Esprima;
using Esprima.Utils;
using Esprima.Ast;

namespace BSDetector {
    public class CodeAnalyzer {
        private string Code;
        private int linesAnalyzed = 0;
        private TooManyParametersFunction TooManyParametersFunctionDetector = new TooManyParametersFunction();
        private TooManyParametersArrowFunction TooManyParametersArrowFunctionDetector = new TooManyParametersArrowFunction();
        private LineTooLong LineTooLongDetector = new LineTooLong();

        public CodeAnalyzer(string Code) {
            this.Code = Code;
        }


        //simplified analysis is performed by iterating over the entire code line by line
        private void SimplifiedAnalysis() {

            string[] lines = Code.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);
            
            int lineNum = 0;

            foreach (var line in lines)
            {
                if (line.Length > 140)
                {
                    LineTooLongDetector.RegisterOccurance(lineNum, 0, lineNum, line.Length - 1);
                }
                lineNum++;
            }

            linesAnalyzed = lineNum;
        }

        private void ASTreeDFS(INode node, int depth) {

            if (node is ArrowFunctionExpression) {
                    var ArrowFunctionNode = (ArrowFunctionExpression)node;
                    if (ArrowFunctionNode.Params.Count > 4) {
                        TooManyParametersArrowFunctionDetector.RegisterOccurance(ArrowFunctionNode.Location.Start.Line, ArrowFunctionNode.Location.Start.Column, ArrowFunctionNode.Location.End.Line, ArrowFunctionNode.Location.End.Column);
                    }
            }

            if (node is FunctionDeclaration) {
                    var RegularFunctionNode = (FunctionDeclaration)node;
                    if (RegularFunctionNode.Params.Count > 5) {
                        TooManyParametersArrowFunctionDetector.RegisterOccurance(RegularFunctionNode.Location.Start.Line, RegularFunctionNode.Location.Start.Column, RegularFunctionNode.Location.End.Line, RegularFunctionNode.Location.End.Column);
                    }
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
            return new FileAnalysisResult {
                LinesAnalyzed=linesAnalyzed,
                SmellsDetected = new Smell[] 
                    {LineTooLongDetector,
                    TooManyParametersFunctionDetector,
                    TooManyParametersArrowFunctionDetector}
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