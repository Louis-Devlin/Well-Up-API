using Microsoft.AspNetCore.Mvc;
using Well_Up_API.Services;
using Well_Up_API.Models;

namespace Well_Up_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitController : ControllerBase
    {
        private readonly HabitService _habitService;

        public HabitController(HabitService habitService)
        {
            _habitService = habitService;
        }

        [HttpGet]
        public List<HabitDTO> GetAllHabits()
        {
            return _habitService.GetAllHabits();
        }

        [HttpPost]
        public IActionResult CreateHabit([FromBody] Habit habit)
        {
            int habitId = _habitService.CreateHabit(habit);
            return CreatedAtAction(nameof(GetAllHabits), new { id = habitId });
        }
    }
}