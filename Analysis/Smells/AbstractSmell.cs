using System.Collections.Generic;

namespace BSDetector
{
    /// <summary>
    /// Represents a detector of a single code smell
    /// </summary>
    public abstract class Smell
    {
        public virtual string SmellName { get; }

        public virtual string SmellDescription { get; }

        public List<Occurrence> Occurrences { get; set; }

        public Smell()
        {
            Occurrences = new List<Occurrence>();
        }

        /// <summary>
        /// Registers occurrence of a code smell at a given position. Note: both lines and columns are counted from 1
        /// </summary>
        /// <param name="LineStart">Line at which the occurrence started</param>
        /// <param name="ColStart">Column at which the occurrence started</param>
        /// <param name="LineEnd">Line at which the occurrence ended</param>
        /// <param name="ColEnd">Column at which the occurrence ended</param>
        public void RegisterOccurrence(int LineStart, int ColStart, int LineEnd, int ColEnd)
        {
            Occurrences.Add(new Occurrence { LineStart = LineStart, ColStart = ColStart, LineEnd = LineEnd, ColEnd = ColEnd, Snippet = "TODO" });
        }

        /// <summary>
        /// Returns all occurrences registered during the detector's lifecycle
        /// </summary>
        /// <returns></returns>
        public Occurrence[] GetOccurrences()
        {
            return Occurrences.ToArray();
        }
    }
}