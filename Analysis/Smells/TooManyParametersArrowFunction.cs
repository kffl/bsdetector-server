namespace BSDetector {
    public class TooManyParametersArrowFunction : Smell {
        public TooManyParametersArrowFunction() {
            SmellName = "Too many parameters for arrow function";
            SmellDescription = "Maximum recommended number of parameters for an arrow function is: 4.";
        }
    }
}