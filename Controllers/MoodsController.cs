using Microsoft.AspNetCore.Mvc;
using Well_Up_API.Models;
using Well_Up_API.Services;

namespace Well_Up_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MoodsController : ControllerBase
    {
        private readonly MoodService _moodService;

        public MoodsController(MoodService moodService) => _moodService = moodService;



        [HttpGet]
        public List<Mood> GetByGroup([FromQuery] string sentiment = "")
        {
            return _moodService.GetMoodGroup(sentiment);
        }

        [HttpPost]
        public IActionResult PopulateDB()
        {
            _moodService.PopulateMood();
            return CreatedAtAction(nameof(GetByGroup), null);
        }
    }
}