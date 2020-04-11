using Esprima.Ast;

namespace BSDetector {
    public abstract class AstSmell : Smell {
        public virtual void AnalyzeNode(INode node, int depth) {
            
        }
    }
}