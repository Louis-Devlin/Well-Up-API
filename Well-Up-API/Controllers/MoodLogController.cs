using Microsoft.AspNetCore.Mvc;
using Well_Up_API.Models;
using Well_Up_API.Services;
using System;

namespace Well_Up_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class MoodLogController : ControllerBase
    {
        private readonly MoodLogService _moodLogService;

        public MoodLogController(MoodLogService moodLogService)
        {
            _moodLogService = moodLogService;
        }

        [HttpPost]
        public IActionResult LogMood([FromBody] MoodLogRequest moodLog)
        {
            var newMood = new MoodLog()
            {
                MoodId = moodLog.MoodId,
                Date = moodLog.Date,
                UserId = moodLog.UserId
            };
            var moodId = _moodLogService.LogMood(newMood);
            return CreatedAtAction(nameof(GetLoggedMood), new { id = moodId }, moodLog);
        }
        [HttpGet]
        public MoodLog GetLoggedMood(int id)
        {
            return _moodLogService.Get(id);
        }
        [Route("/User/{id}")]
        [HttpGet]
        public List<UserLogResponse> GetByUser(int id)
        {
            return _moodLogService.GetByUser(id);
        }
        [Route("totals/{id}")]
        [HttpGet]
        public Dictionary<string, int> GetWeeklyTotals(int id)
        {
            return _moodLogService.GetWeeklyTotals(id);
        }

        [Route("totals/{id}/{date}")]
        [HttpGet]
        public List<MoodLogCountResponse> GetTotalsByDay(int id, DateTime date)
        {
            return _moodLogService.GetTotalsByDay(id, date);
        }
    }
}