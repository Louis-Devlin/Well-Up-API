using Well_Up_API.Migrations;
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

        public Dictionary<string, int> GetLoggedHabits(int userId)
        {
            var habits = _context.HabitLog.Where(h => h.UserId == userId).ToList();
            Dictionary<string, int> habitLog = new Dictionary<string, int>();
            foreach (HabitLog habit in habits)
            {
                string id = habit.HabitId.ToString();
                if (habitLog.ContainsKey(id))
                {
                    habitLog[id]++;
                }
                else
                {
                    habitLog[id] = 1;
                }
            }
            return habitLog;
        }

        public int LogHabit(HabitLog log)
        {
            _context.HabitLog.Add(log);
            _context.SaveChanges();
            return log.HabitLogId;
        }
    }
}