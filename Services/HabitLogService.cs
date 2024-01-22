using Well_Up_API.Models;

namespace Well_Up_API.Services
{
    public class HabitLogService
    {
        private readonly PostgresDbContext _context;
        public HabitLogService(PostgresDbContext context)
        {
            _context = context;
        }

        public List<HabitLog> GetLoggedHabits(int userId)
        {
            return _context.HabitLog.Where(x => x.UserId == userId).ToList();
        }

        public int LogHabit(HabitLog log)
        {
            _context.HabitLog.Add(log);
            _context.SaveChanges();
            return log.HabitLogId;
        }
        public Dictionary<string, int> GetWeeklyTotals(int userId)
        {
            var startDate = DateTime.Now.AddDays(-7);
            var habits = _context.HabitLog.Where(m => m.Date >= startDate && m.UserId == userId).ToList();
            var totals = new Dictionary<string, int>(){
                {"Monday",0}
                ,{"Tuesday",0}
                ,{"Wednesday",0}
                ,{"Thursday",0}
                ,{"Friday",0}
                ,{"Saturday",0}
                ,{"Sunday",0}
            };
            foreach (var habit in habits)
            {
                var date = habit.Date;
                var day = date.DayOfWeek.ToString();
                if (totals.ContainsKey(day))
                {
                    totals[day] += 1;
                }
            }
            return totals;
        }
    }
}