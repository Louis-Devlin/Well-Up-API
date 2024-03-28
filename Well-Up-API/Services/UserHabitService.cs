
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

        public List<UserHabitDTO> GetUserHabitsByDate(int userId, DateTime date, bool active)
        {
            var habits = new List<int>();
            if (active)
            {
                habits = _context.UserHabit.Where(x => x.UserId == userId && x.Active).Select(o => o.HabitId).ToList();
            }
            else
            {
                habits = _context.UserHabit.Where(x => x.UserId == userId).Select(o => o.HabitId).ToList();
            }
            Dictionary<int, int> habitDictionary = habits.ToDictionary(habitId => habitId, _ => 0);
            var logged = _context.HabitLog.Where(x => x.UserId == userId && habits.Contains(x.HabitId) && x.Date.Date == date.Date);

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
        public void StopTrackingHabitAndDeleteAllLogs(int userId, int habitId)
        {
            var habit = _context.UserHabit.FirstOrDefault(x => x.UserId == userId && x.HabitId == habitId);
            if (habit != null)
            {
                _context.UserHabit.Remove(habit);
                var loggedHabits = _context.HabitLog.Where(x => x.UserId == userId && x.HabitId == habitId);
                foreach (var log in loggedHabits)
                {
                    _context.HabitLog.Remove(log);
                }
                _context.SaveChanges();
            }

        }

        public void StopTrackingHabit(int userId, int habitId)
        {
            var userHabit = _context.UserHabit.Where(x => x.UserId == userId && x.HabitId == habitId).FirstOrDefault();
            if (userHabit != null)
            {
                userHabit.Active = false;
                _context.SaveChanges();
            }
        }
        private int TrackHabit(UserHabit userHabit)
        {
            _context.UserHabit.Add(userHabit);
            _context.SaveChanges();
            return userHabit.UserHabitId;
        }
        private List<UserHabitDTO> PrepareResponse(Dictionary<int, int> dict)
        {
            List<UserHabitDTO> habitLog = new List<UserHabitDTO>();
            var keys = dict.Keys.ToList();
            var loggedHabits = _context.Habit.Where(h => keys.Contains(h.HabitId));
            foreach (var logged in loggedHabits)
            {
                habitLog.Add(new UserHabitDTO()
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