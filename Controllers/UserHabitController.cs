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

        [HttpGet(Name = "GetHabitsByUser")]
        public List<UserHabitDTO> GetHabitsByUser(int id, DateTime date)
        {
            return _userHabitService.GetUserHabitsByDate(id, date);
        }
        [HttpPost]
        public IActionResult StartTrackingHabit([FromBody] UserHabitRequest userHabit)
        {
            int userHabitId = _userHabitService.StartTrackingHabit(userHabit);
            return CreatedAtAction("GetHabitsByUser", new { id = userHabitId }, null);
        }

        [HttpDelete]
        public IActionResult StopTrackingHabitAndDeleteAllLogs(int userId, int habitId)
        {
            _userHabitService.StopTrackingHabitAndDeleteAllLogs(userId, habitId);
            return NoContent();
        }

        [HttpPut("{habitTrackId}")]

        public IActionResult StopTrackingHabit(int habitTrackId)
        {
            _userHabitService.StopTrackingHabit(habitTrackId);
            return NoContent();
        }
    }
}