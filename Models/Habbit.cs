namespace Well_Up_API.Models
{
    public class Habbit
    {
        public int HabbitId { get; set; }
        public string? HabbitName { get; set; }
        public ICollection<HabitLog>? HabbitLogs { get; set; }
    }
}