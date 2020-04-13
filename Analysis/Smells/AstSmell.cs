using Esprima.Ast;
using Esprima;

namespace BSDetector
{
    public abstract class AstSmell : Smell
    {
        public virtual void AnalyzeNode(INode node, int depth)
        {

        }
        public void RegisterOccurrence(Location loc)
        {
            RegisterOccurrence(loc.Start.Line, loc.Start.Column, loc.End.Line, loc.End.Column);
        }
    }
}