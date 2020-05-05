namespace BSDetector
{
    /// <summary>
    /// Represents a detector of a line smell
    /// Line smells are detected by analyzing the input code line-by-line
    /// </summary>
    public abstract class LineSmell : Smell
    {
        /// <summary>
        /// Performs analysis of a single line of code. Gets triggered for each line in analyzed code
        /// </summary>
        /// <param name="currentLine">Contents of the line that is being analyzed</param>
        /// <param name="previousLine">Contents of the line before the current line</param>
        /// <param name="lineNum">Number of the analyzed line</param>
        public virtual void AnalyzeLine(string currentLine, string previousLine, int lineNum)
        {

        }
    }
}