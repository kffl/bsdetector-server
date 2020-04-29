using System.Collections.Generic;

namespace BSDetector
{
    public abstract class Smell
    {

        public string SmellName { get; set; }
        public string SmellDescription { get; set; }
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