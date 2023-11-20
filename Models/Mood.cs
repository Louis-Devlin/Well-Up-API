using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Well_Up_API.Models
{
    public class Mood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MoodId { get; set; }

        public string? MoodName { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public string? Colour {get;set;}

        public ICollection<MoodLog>? MoodLogs { get; set; }

    }
}

