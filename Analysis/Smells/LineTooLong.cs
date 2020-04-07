namespace BSDetector {
    public class LineTooLong : Smell {
        public LineTooLong() {
            SmellName = "Line too long";
            SmellDescription = "Lines that are too long make your code less readable.";
        }
    }
}