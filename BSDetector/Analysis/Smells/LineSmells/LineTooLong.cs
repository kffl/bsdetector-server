namespace BSDetector
{
    public class LineTooLong : LineSmell
    {
        public override string SmellName
        {
            get
            {
                return "LINE_TOO_LONG";
            }
        }

        public override int snippetContextBefore { get { return 0; } }

        public override void AnalyzeLine(string currentLine, string previousLine, int lineNum)
        {
            if (currentLine.Length > 140)
            {
                RegisterOccurrence(lineNum, 1, lineNum, currentLine.Length);
            }
        }
    }
}