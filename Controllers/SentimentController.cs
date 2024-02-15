using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

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
    }
}