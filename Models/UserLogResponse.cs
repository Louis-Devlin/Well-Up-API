namespace Well_Up_API.Models
{
    public class UserLogResponse
    {
        public DateTime Date { get; set; }
        public UserLogDataResponse Data { get; set; }
    }

    public class UserLogDataResponse
    {
        public List<MoodLogResponse> MoodLog { get; set; }
        public List<HabitLog> HabitLog { get; set; }
    }
}