using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Well_Up_API.Models
{
    public class HabitLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HabitLogId { get; set; }
        public int UserId { get; set; }
        public int HabitId { get; set; }
        public DateTime Date { get; set; }
        public User? User { get; set; }
        public Habit? Habit { get; set; }

        
    }
}