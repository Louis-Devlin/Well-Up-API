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

        public int LogMood(MoodLog moodLog){
            _context.MoodLog.AddAsync(moodLog);
            _context.SaveChanges();

            return moodLog.MoodId;
        }
        public MoodLog Get(int id){
            return _context.MoodLog.Where(m => m.MoodLogId == id).First();
        }
    }
}