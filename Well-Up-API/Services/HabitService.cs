using Well_Up_API.Models;

namespace Well_Up_API.Services
{
    public class HabitService
    {
        private readonly PostgresDbContext _context;

        public HabitService(PostgresDbContext postgresDbContext)
        {
            _context = postgresDbContext;
        }

        public List<HabitDTO> GetAllHabits()
        {
            return _context.Habit.Select(h => new HabitDTO { HabitId = h.HabitId, HabitName = h.HabitName }).ToList();
        }
        public int CreateHabit(Habit habit)
        {
            _context.Habit.AddAsync(habit);
            _context.SaveChanges();
            return habit.HabitId;
        }
    }
}