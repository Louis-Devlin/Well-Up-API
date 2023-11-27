
using Well_Up_API.Models;

namespace Well_Up_API.Services
{
    public class UserHabitService
    {
        private readonly PostgresDbContext _context;

        public UserHabitService(PostgresDbContext context)
        {
            _context = context;
        }
        public List<Habit> GetUserHabits(int userId)
        {
            var habits = _context.UserHabit.Where(x => x.UserId == userId).Select(o => o.HabitId).ToList();
            return _context.Habit.Where(h => habits.Contains(h.HabitId)).ToList();
        }
        public int StartTrackingHabit(UserHabitRequest userHabit)
        {
            int id = -1;
            if (userHabit.HabitId != -1)
            {
                id = TrackHabit(new UserHabit()
                {
                    UserId = userHabit.UserId,
                    HabitId = userHabit.HabitId
                });
            }
            if (!String.IsNullOrWhiteSpace(userHabit.HabitName))
            {
                var newHabit = new Habit()
                {
                    HabitName = userHabit.HabitName
                };
                _context.Habit.Add(newHabit);
                id = TrackHabit(new UserHabit()
                {
                    UserId = userHabit.UserId,
                    HabitId = userHabit.HabitId
                });
            }
            return id;
        }
        public void StopTrackingHabit(int userId, int habitId)
        {
            _context.UserHabit.Where(x => x.UserId == userId && x.HabitId == habitId);
            _context.SaveChanges();
        }
        private int TrackHabit(UserHabit userHabit)
        {
            _context.UserHabit.Add(userHabit);
            _context.SaveChanges();
            return userHabit.UserHabitId;
        }
    }
}