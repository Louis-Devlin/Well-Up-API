
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
        public List<UserLogResponse> GetByUser(int id)
        {
            var moodLog = _context.MoodLog.Where(m => m.UserId == id).ToList();
            var habitLogs = _context.HabitLog.Where(h => h.UserId == id).ToList();

            var groupedMoodLog = moodLog.GroupBy(m => m.Date.Date).ToDictionary(g => g.Key, g => g.ToList());
            var groupedHabitLog = habitLogs.GroupBy(h => h.Date.Date).ToDictionary(g => g.Key, g => g.ToList());

            var userLog = new List<UserLogResponse>();

            var allDates = new HashSet<DateTime>(groupedMoodLog.Keys.Union(groupedHabitLog.Keys));

            foreach (var date in allDates)
            {
                List<MoodLogResponse> moodLogResponse;
                if (groupedMoodLog.ContainsKey(date))
                {
                    var moodIds = groupedMoodLog[date].Select(m => m.MoodId).ToList();
                    var moods = _context.Mood.Where(m => moodIds.Contains(m.MoodId)).ToList();

                    moodLogResponse = groupedMoodLog[date].Select(m =>
                   {
                       var mood = moods.First(mood => mood.MoodId == m.MoodId);
                       if (mood == null)
                       {
                           return null;
                       }
                       return new MoodLogResponse()
                       {
                           MoodName = mood.MoodName,
                           Date = m.Date,
                           Color = GetColor(mood.PositionX, mood.PositionY)
                       };
                   }).Where(m => m != null).ToList();
                }
                else
                {
                    moodLogResponse = new List<MoodLogResponse>();
                }
                List<HabitLog> habitLogResponse;

                if (groupedHabitLog.ContainsKey(date))
                {
                    habitLogResponse = groupedHabitLog[date];
                }
                else
                {
                    habitLogResponse = new List<HabitLog>();
                }
                userLog.Add(new UserLogResponse
                {
                    Date = date,
                    Data = new UserLogDataResponse
                    {
                        MoodLog = moodLogResponse,
                        HabitLog = habitLogResponse
                    }
                });
            }
            return userLog;
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

        public List<MoodLogCountResponse> GetTotalsByDay(int userId, DateTime date)
        {

            Console.WriteLine(userId);
            Console.WriteLine(date);
            List<MoodLogCountResponse> totals = new List<MoodLogCountResponse>();
            var log = _context.MoodLog.Where(m => m.UserId == userId && m.Date.Date == date.Date).ToList();
            List<MoodLogResponse> moods = new List<MoodLogResponse>();
            foreach (var entry in log)
            {
                var mood = _context.Mood.Where(m => m.MoodId == entry.MoodId).First();
                moods.Add(new MoodLogResponse()
                {
                    MoodName = mood.MoodName,
                    Date = entry.Date,
                    Color = GetColor(mood.PositionX, mood.PositionY)
                });

            }
            foreach (var item in moods)
            {
                var mood = totals.FirstOrDefault(m => m.MoodName == item.MoodName);
                if (mood != null)
                {
                    mood.Count++;
                }
                else
                {
                    totals.Add(new MoodLogCountResponse()
                    {
                        MoodName = item.MoodName,
                        Count = 1,
                        Colour = item.Color
                    });
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