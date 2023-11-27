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

        public List<HabitLogDTO> GetLoggedHabits(int userId)
        {
            var habits = _context.HabitLog.Where(h => h.UserId == userId).ToList();
            Dictionary<int, int> habitLog = new Dictionary<int, int>();
            foreach (var habit in habits.Select(h => h.HabitId))
            {
                if (habitLog.ContainsKey(habit))
                {
                    habitLog[habit]++;
                }
                else
                {
                    habitLog[habit] = 1;
                }
            }
            return PrepareResponse(habitLog);
        }

        public int LogHabit(HabitLog log)
        {
            _context.HabitLog.Add(log);
            _context.SaveChanges();
            return log.HabitLogId;
        }

        public List<HabitLogDTO> PrepareResponse(Dictionary<int, int> dict)
        {
            List<HabitLogDTO> habitLog = new List<HabitLogDTO>();
            var keys = dict.Keys.ToList();
            var loggedHabits = _context.Habit.Where(h => keys.Contains(h.HabitId));
            foreach (var logged in loggedHabits)
            {
                habitLog.Add(new HabitLogDTO()
                {
                    HabbitId = logged.HabitId,
                    HabitName = logged.HabitName,
                    Count = dict[logged.HabitId]
                });
            }
            return habitLog;
        }
    }
}