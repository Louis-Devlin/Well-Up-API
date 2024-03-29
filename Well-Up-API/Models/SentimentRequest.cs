using Newtonsoft.Json;
namespace Well_Up_API.Models
{
    public class SentimentRequest
    {
        [JsonProperty("text")]
        public string? Text { get; set; }
        [JsonProperty("sentiment")]
        public string? Sentiment { get; set; }
    }
}