namespace Well_Up_API.Models
{
    public class UserHabitRequest
    {
        public int UserId { get; set; }
        public int HabitId { get; set; }
        public string? HabitName { get; set; }
    }
}