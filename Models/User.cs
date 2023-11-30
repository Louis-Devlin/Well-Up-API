using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Well_Up_API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<MoodLog> MoodLogs { get; set; }
        public ICollection<HabitLog> HabitLogs { get; set; }
        public ICollection<UserHabit> UserHabits { get; set; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}