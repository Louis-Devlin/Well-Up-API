namespace Well_Up_API.Models
{
    public class UserHabit
    {
        public int UserHabitId { get; set; }
        public int UserId { get; set; }
        public int HabitId { get; set; }
        public User? User { get; set; }
        public Habit? Habit { get; set; }

    }
}