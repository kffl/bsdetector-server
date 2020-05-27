using System.Collections.Generic;

namespace BSDetector
{
    public class DuplicatedCode : LineSmell
    {
        public override string SmellName => "DUPLICATED_CODE";

        public override int snippetContextBefore { get { return 1; } }
        HashSet<int> hashCodes = new HashSet<int>();

        public override void AnalyzeLine(string currentLine, string previousLine, int lineNum)
        {
            if (!hashCodes.Add(currentLine.GetHashCode()))
            {
                RegisterOccurrence(lineNum, 1, lineNum, currentLine.Length);
            }
        }
    }
}