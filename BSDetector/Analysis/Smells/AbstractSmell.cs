using System.Collections.Generic;

namespace BSDetector
{
    /// <summary>
    /// Represents a detector of a single code smell
    /// </summary>
    public abstract class Smell
    {
        public virtual string SmellName { get; }
        public List<Occurrence> Occurrences { get; set; }
        /// <summary>
        /// Determines how many lines before the smell occurrence shall be included
        /// in the smell snippet for context
        /// </summary>
        /// <value>Number of prepending lines in occurrence snippet</value>
        public virtual int snippetContextBefore { get { return 0; } }
        /// <summary>
        /// Determines how many lines after the smell occurrence shall be included
        /// in the smell snippet for context
        /// </summary>
        /// <value>Number of trailing lines in occurrence snippet</value>
        public virtual int snippetContextAfter { get { return 0; } }

        /// <summary>
        /// Initializes new smell detector
        /// </summary>
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
            Occurrences.Add(new Occurrence { LineStart = LineStart, ColStart = ColStart, LineEnd = LineEnd, ColEnd = ColEnd });
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