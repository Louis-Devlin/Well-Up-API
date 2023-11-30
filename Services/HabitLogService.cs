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
    }
}