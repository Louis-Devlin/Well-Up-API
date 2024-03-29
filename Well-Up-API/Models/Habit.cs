using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Well_Up_API.Models
{
    public class Habit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HabitId { get; set; }
        public string? HabitName { get; set; }
        public ICollection<HabitLog>? HabitLogs { get; set; }
        public ICollection<UserHabit> UserHabits { get; set; }
    }
}