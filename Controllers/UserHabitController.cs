using Microsoft.AspNetCore.Mvc;
using Well_Up_API.Models;
using Well_Up_API.Services;

namespace Well_Up_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHabitController : ControllerBase
    {
        private readonly UserHabitService _userHabitService;

        public UserHabitController(UserHabitService userHabitService) => _userHabitService = userHabitService;

        [HttpGet("{id}", Name = "GetHabitsByUser")]
        public List<Habit> GetHabitsByUser(int id)
        {
            return _userHabitService.GetUserHabits(id);
        }
        [HttpPost]
        public IActionResult StartTrackingHabit([FromBody] UserHabitRequest userHabit)
        {
            int userHabitId = _userHabitService.StartTrackingHabit(userHabit);
            return CreatedAtAction("GetHabitsByUser", new { id = userHabitId }, null);
        }

        [HttpDelete]
        public IActionResult StopTrackingHabit(int userId, int habitId)
        {
            _userHabitService.StopTrackingHabit(userId, habitId);
            return NoContent();
        }
    }
}