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
        public List<UserHabitDTO> GetHabitsByUser(int id, DateTime date, bool active)
        {
            return _userHabitService.GetUserHabitsByDate(id, date, active);
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

        [HttpPut]
        public IActionResult StopTrackingHabit(int userId, int habitId)
        {
            _userHabitService.StopTrackingHabit(userId, habitId);
            return NoContent();
        }
    }
}