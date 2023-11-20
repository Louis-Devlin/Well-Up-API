
using System.Text.Json;
using Well_Up_API.Models;

namespace Well_Up_API.Services
{
    public class MoodService
    {
        private readonly PostgresDbContext _context;

        public MoodService(PostgresDbContext context)
        {
            _context = context;
        }

        public List<Mood> GetMoods()
        {
            return _context.Mood.ToList();
        }
        public List<Mood> GetMoodGroup(int value)
        {
            if (value == 0)
            {
                return _context.Mood.Where(y => y.PositionY == value).ToList();
            }
            return _context.Mood.Where(y => y.PositionY == value - 1).ToList();
        }
        public void PopulateMood()
        {
            string text = File.ReadAllText(@"./moods.json");
            var Moods = JsonSerializer.Deserialize<List<Mood>>(text);
            foreach (var mood in Moods)
            {
                mood.Colour = (mood.PositionX >= 5) ? ((mood.PositionY >= 5) ? "green" : "blue") : ((mood.PositionY >= 5) ? "yellow" : "red");
            }
            if (Moods.Any())
            {
                _context.Mood.AddRange(Moods);
                _context.SaveChanges();
            }

        }
    }
}