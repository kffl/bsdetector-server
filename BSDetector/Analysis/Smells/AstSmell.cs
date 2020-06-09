using Esprima;
using Esprima.Ast;

namespace BSDetector
{
    /// <summary>
    /// Represents a detector of an AST smell
    /// AST smells are detected when analysing code's AST
    /// </summary>
    public abstract class AstSmell : Smell
    {
        /// <summary>
        /// Function that gets triggered for each node of the AST
        /// </summary>
        /// <param name="node">AST node that is being analyzed</param>
        /// <param name="depth">Depth at which the current node is located in the AST</param>
        public virtual void AnalyzeNode(INode node, int depth)
        {
        }

        public virtual void AnalyzeNode(INode node, int depth, Location location)
        {
        }

        /// <summary>
        /// Registers a smell occurrence from a location object
        /// </summary>
        /// <param name="loc">Location object representing occurrence in code</param>
        public void RegisterOccurrence(Location loc)
        {
            RegisterOccurrence(loc.Start.Line, loc.Start.Column + 1, loc.End.Line, loc.End.Column + 1);
        }

        /// <summary>
        /// Registers a smell occurrence in a range of nodes (between the first node and the last node)
        /// </summary>
        /// <param name="nodes">Range of nodes</param>
        public void RegisterOccurrence(NodeList<INode> nodes)
        {
            INode firstNode = null;
            INode lastNode = null;
            var i = 0;
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