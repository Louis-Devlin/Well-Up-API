using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Well_Up_API.Models
{
    public class UserHabit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserHabitId { get; set; }
        public int UserId { get; set; }
        public int HabitId { get; set; }
        public User? User { get; set; }
        public Habit? Habit { get; set; }

        public bool Active { get; set; } = true;

    }
}