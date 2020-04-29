namespace BSDetector
{
    public class LineTooLong : LineSmell
    {
        public LineTooLong()
        {
            SmellName = "Line too long";
            SmellDescription = "Lines that are too long make your code less readable.";
        }
        public override void AnalyzeLine(string currentLine, string previousLine, int lineNum)
        {
            if (currentLine.Length > 140)
            {
                RegisterOccurrence(lineNum, 1, lineNum, currentLine.Length);
            }
        }
    }
}