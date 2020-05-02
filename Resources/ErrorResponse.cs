using System.Text.Json.Serialization;

namespace BSDetector.Resources

{
    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        public string errorName { get; set; }
        public string message { get; set; }
    }

    public class ParseErrorResponse : ErrorResponse
    {
        public int line { get; set; }
        public int column { get; set; }
    }
}