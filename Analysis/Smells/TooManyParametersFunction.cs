namespace BSDetector {
    public class TooManyParametersFunction : Smell {
        public TooManyParametersFunction() {
            SmellName = "Too many parameters for a function declaration";
            SmellDescription = "Maximum recommended number of parameters for a regular function is: 5.";
        }
    }
}