using System.Collections.Generic;

namespace BSDetector {
    public abstract class Smell {

        public string SmellName {get; set;}
        public string SmellDescription {get; set;}
        public List<Occurance> Occurances {get; set;}

        public Smell()
        {
            Occurances = new List<Occurance>();
        }
        public void RegisterOccurance (int LineStart, int ColStart, int LineEnd, int ColEnd) {
            Occurances.Add (new Occurance { LineStart = LineStart, ColStart = ColStart, LineEnd = LineEnd, ColEnd = ColEnd, Snippet = "TODO" });
        }
        public Occurance[] GetOccurances () {
            return Occurances.ToArray ();
        }
    }
}