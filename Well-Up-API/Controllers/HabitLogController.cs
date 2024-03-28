using Microsoft.AspNetCore.Mvc;
using Well_Up_API.Models;
using Well_Up_API.Services;

namespace Well_Up_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HabitLogController : ControllerBase
    {
        private readonly HabitLogService _habitLogService;

        public HabitLogController(HabitLogService habitLogService)
        {
            _habitLogService = habitLogService;
        }

        [HttpGet]
        public List<HabitLog> GetAll()
        {
            return new List<HabitLog>();
        }

        [HttpGet("{id}")]
        public List<HabitLog> GetAllLoggedHabits(int id)
        {
            return _habitLogService.GetLoggedHabits(id);
        }

        [HttpPost]
        public IActionResult LogHabit([FromBody] HabitLog completedHabit)
        {
            int habitId = _habitLogService.LogHabit(completedHabit);
            return CreatedAtAction(nameof(GetAll), new { id = habitId });
        }
        [Route("totals/{id}")]
        [HttpGet]
        public Dictionary<string, int> GetWeeklyTotals(int id)
        {
            return _habitLogService.GetWeeklyTotals(id);
        }

    }
}