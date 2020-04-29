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
            RegisterOccurrence(loc.Start.Line, loc.Start.Column + 1, loc.End.Line, loc.End.Column + 1);
        }
        public void RegisterOccurrence(NodeList<INode> nodes)
        {
            INode firstNode = null;
            INode lastNode = null;
            int i = 0;
            foreach (var node in nodes)
            {
                if (i++ == 0)
                    firstNode = node;
                lastNode = node;
            }
            RegisterOccurrence(firstNode.Location.Start.Line, firstNode.Location.Start.Column + 1,
                                lastNode.Location.End.Line, lastNode.Location.End.Column + 1);
        }
    }
}