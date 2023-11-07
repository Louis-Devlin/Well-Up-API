using Microsoft.AspNetCore.SignalR;

namespace Well_Up_API.Models
{
    public class MoodLogRequest
    {
        public int MoodId { get; set; }
        public int UserId { get; set; }

        public DateTime Date { get; set; }
    }
}