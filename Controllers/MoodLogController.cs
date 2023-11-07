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
            Console.WriteLine("HIT CREATE LOGMOOD");
            Console.WriteLine($"MoodID: {moodLog.MoodId}");
            Console.WriteLine($"Date: {moodLog.Date}");
            Console.WriteLine($"UserId: {moodLog.UserId}");


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
        public List<MoodLogResponse> GetByUser(int id)
        {
            return _moodLogService.GetByUser(id);
        }

    }
}