
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
        public List<Mood> GetMoodGroup(string value)
        {
            List<Mood> moods = new List<Mood>();
            switch (value)
            {
                case "positive":
                    moods = _context.Mood.Where(mood => mood.PositionY >= 7 && mood.PositionY <= 9).ToList();
                    break;
                case "negative":
                    moods = _context.Mood.Where(mood => mood.PositionY >= 4 && mood.PositionY <= 6).ToList();
                    break;
                case "netural": // Neutral 
                    moods = _context.Mood.Where(mood => mood.PositionY >= 0 && mood.PositionY <= 3).ToList();
                    break;
                default:
                    moods = _context.Mood.ToList();
                    break;

            }
            return SortMoodsByPositionY(moods);
        }
        public List<Mood> SortMoodsByPositionY(List<Mood> moods)
        {
            return moods.OrderBy(mood => mood.PositionY).ToList();
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