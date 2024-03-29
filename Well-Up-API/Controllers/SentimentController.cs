using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Well_Up_API.Models;

namespace Well_Up_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentimentController : ControllerBase
    {
        private static HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://well-up-ml-mudo5ec2va-nw.a.run.app")
        };
        [HttpGet]
        [Route("sentimentprediction")]
        public async Task<string> GetSentiment([FromQuery] string sentimentText)
        {
            using (var response = await client.GetAsync($"sentiment?message={sentimentText}"))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonDocument.Parse(json).RootElement;
                var propertyValue = jsonObject.GetProperty("predicted_sentiment").GetString();
                return propertyValue;
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddSentiment([FromBody] SentimentRequest sentimentRequest)
        {
            using StringContent json = new(JsonSerializer.Serialize(sentimentRequest), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync("sentiment", json))
            {
                response.EnsureSuccessStatusCode();
                return Created("api/sentiment", "Sentiment added successfully!");
            }
        }
    }
}