using System.Collections.Generic;

namespace BSDetector
{
    public abstract class Smell
    {

        public virtual string SmellName { get; }

        public virtual string SmellDescription { get; }

        public List<Occurrence> Occurrences { get; set; }

        public Smell()
        {
            Occurrences = new List<Occurrence>();
        }
        public void RegisterOccurrence(int LineStart, int ColStart, int LineEnd, int ColEnd)
        {
            Occurrences.Add(new Occurrence { LineStart = LineStart, ColStart = ColStart, LineEnd = LineEnd, ColEnd = ColEnd, Snippet = "TODO" });
        }
        public Occurrence[] GetOccurrences()
        {
            return Occurrences.ToArray();
        }
    }
}