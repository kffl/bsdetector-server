namespace BSDetector {
    public class LineTooLong : LineSmell {
        public LineTooLong() {
            SmellName = "Line too long";
            SmellDescription = "Lines that are too long make your code less readable.";
        }
        public override void AnalyzeLine(string currentLine, string previousLine, int lineNum) {
            if (currentLine.Length > 140)
            {
                RegisterOccurance(lineNum, 0, lineNum, currentLine.Length - 1);
            }
        }
    }
}