using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Well_Up_API.Models
{
    public class MoodLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MoodLogId { get; set; }

        public int MoodId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public Mood? Mood { get; set; }

        public User? User { get; set; }

    }
}