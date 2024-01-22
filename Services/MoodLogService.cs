
using Well_Up_API.Models;

namespace Well_Up_API.Services
{

    public class MoodLogService
    {
        private readonly PostgresDbContext _context;

        public MoodLogService(PostgresDbContext postgresDbContext)
        {
            _context = postgresDbContext;
        }

        public int LogMood(MoodLog moodLog)
        {
            _context.MoodLog.AddAsync(moodLog);
            _context.SaveChanges();

            return moodLog.MoodId;
        }
        public MoodLog Get(int id)
        {
            return _context.MoodLog.Where(m => m.MoodLogId == id).First();
        }
        public List<MoodLogResponse> GetByUser(int id)
        {
            var list = _context.MoodLog.Where(m => m.UserId == id).ToList();
            List<MoodLogResponse> send = new List<MoodLogResponse>();
            foreach (var item in list)
            {
                var mood = _context.Mood.Where(m => m.MoodId == item.MoodId).First();
                send.Add(new MoodLogResponse()
                {
                    MoodName = mood.MoodName,
                    Date = item.Date,
                    Color = GetColor(mood.PositionX, mood.PositionY)
                });
            }
            return send;
        }
        public Dictionary<string, int> GetWeeklyTotals(int userId)
        {
            Dictionary<string, int> totals = new Dictionary<string, int>(){
                {"red", 0},
                {"yellow", 0},
                {"blue", 0},
                {"green", 0}
            };
            var startDate = DateTime.Now.AddDays(-7);
            var list = _context.MoodLog.Where(m => m.Date >= startDate && m.UserId == userId).ToList();
            List<MoodLogResponse> send = new List<MoodLogResponse>();
            foreach (var item in list)
            {
                var mood = _context.Mood.Where(m => m.MoodId == item.MoodId).First();
                send.Add(new MoodLogResponse()
                {
                    MoodName = mood.MoodName,
                    Date = item.Date,
                    Color = GetColor(mood.PositionX, mood.PositionY)
                });
            }
            foreach (var item in send)
            {
                if (totals.ContainsKey(item.Color))
                {
                    totals[item.Color]++;
                }
            }
            return totals;
        }
        private string GetColor(int x, int y)
        {
            if (x >= 5)
            {
                return y >= 5 ? "green" : "blue";
            }
            else
            {
                return y >= 5 ? "yellow" : "red";
            }
        }
    }
}