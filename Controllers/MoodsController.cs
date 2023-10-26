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
        public List<Mood> PopulateDB()
        {
            _moodService.PopulateMood();
            return _moodService.GetMoods();
        }

        [HttpGet("{value}")]
        public List<Mood> GetByGroup(int value)
        {
            Console.WriteLine($"Value is {value}");
            return _moodService.GetMoodGroup(value);
        }
    }

}