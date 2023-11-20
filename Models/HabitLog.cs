namespace Well_Up_API.Models
{
    public class HabitLog
    {
        public int HabbitLogId { get; set; }
        public int UserId { get; set; }
        public int HabbitId { get; set; }
        public DateTime Date { get; set; }
        public User? User { get; set; }
        public Habbit? Habbit { get; set; }


    }
}