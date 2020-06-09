namespace BSDetector
{
    /// <summary>
    /// Represents a single occurrence of a given code smell
    /// </summary>
    public class Occurrence
    {
        public string Snippet { get; set; }
        public int LineStart { get; set; }
        public int ColStart { get; set; }
        public int LineEnd { get; set; }
        public int ColEnd { get; set; }
    }
}