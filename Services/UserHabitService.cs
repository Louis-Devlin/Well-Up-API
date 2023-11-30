
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
        public List<HabitLogDTO> GetUserHabits(int userId)
        {
            var habits = _context.UserHabit.Where(x => x.UserId == userId).Select(o => o.HabitId).ToList();
            Dictionary<int, int> habitDictionary = habits.ToDictionary(habitId => habitId, _ => 0);
            var logged = _context.HabitLog.Where(x => x.UserId == userId && habits.Contains(x.HabitId));
            foreach (var habit in logged.Select(h => h.HabitId))
            {
                if (habitDictionary.ContainsKey(habit))
                {
                    habitDictionary[habit]++;
                }
            }
            return PrepareResponse(habitDictionary);
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
                _context.SaveChanges();
                id = TrackHabit(new UserHabit()
                {
                    UserId = userHabit.UserId,
                    HabitId = newHabit.HabitId
                });
            }
            return id;
        }
        public void StopTrackingHabit(int userId, int habitId)
        {
            var habbit = _context.UserHabit.FirstOrDefault(x => x.UserId == userId && x.HabitId == habitId);
            if (habbit != null)
            {
                _context.UserHabit.Remove(habbit);
                _context.SaveChanges();
            }

        }
        private int TrackHabit(UserHabit userHabit)
        {
            _context.UserHabit.Add(userHabit);
            _context.SaveChanges();
            return userHabit.UserHabitId;
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
                    HabitId = logged.HabitId,
                    HabitName = logged.HabitName,
                    Count = dict[logged.HabitId]
                });
            }
            return habitLog;
        }
    }
}