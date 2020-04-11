using System.Collections.Generic;

namespace BSDetector
{
    public abstract class Smell
    {

        public string SmellName { get; set; }
        public string SmellDescription { get; set; }
        public List<Occurence> Occurences { get; set; }

        public Smell()
        {
            Occurences = new List<Occurence>();
        }
        public void RegisterOccurence(int LineStart, int ColStart, int LineEnd, int ColEnd)
        {
            Occurences.Add(new Occurence { LineStart = LineStart, ColStart = ColStart, LineEnd = LineEnd, ColEnd = ColEnd, Snippet = "TODO" });
        }
        public Occurence[] GetOccurences()
        {
            return Occurences.ToArray();
        }
    }
}