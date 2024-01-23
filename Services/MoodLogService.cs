
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
                List<MoodLogCountResponse> moodLogResponse;
                if (groupedMoodLog.ContainsKey(date))
                {
                    var moodIds = groupedMoodLog[date].Select(m => m.MoodId).Distinct().ToList();
                    var moods = _context.Mood.Where(m => moodIds.Contains(m.MoodId)).ToList();

                    moodLogResponse = moods.Select(m => new MoodLogCountResponse
                    {
                        MoodName = m.MoodName,
                        Colour = GetColor(m.PositionX, m.PositionY),
                        Count = groupedMoodLog[date].Count(mood => mood.MoodId == m.MoodId)
                    }).ToList();
                }
                else
                {
                    moodLogResponse = new List<MoodLogCountResponse>();
                }
                List<UserHabitDTO> habitLogResponse;

                if (groupedHabitLog.ContainsKey(date))
                {
                    habitLogResponse = groupedHabitLog[date]
                        .GroupBy(h => h.HabitId)
                        .Select(group => new UserHabitDTO
                        {
                            HabitId = group.Key,
                            HabitName = _context.Habit.Where(habit => habit.HabitId == group.Key).First().HabitName,
                            Count = group.Count()
                        })
                        .ToList();
                }
                else
                {
                    habitLogResponse = new List<UserHabitDTO>();
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