using Microsoft.EntityFrameworkCore;

namespace Well_Up_API.Models
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }

        public DbSet<Mood> Mood { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<MoodLog> MoodLog { get; set; }

        public DbSet<Habit> Habit { get; set; }
        public DbSet<HabitLog> HabitLog { get; set; }
        public DbSet<UserHabit> UserHabit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MoodLog>().HasKey(ml => ml.MoodLogId);
            modelBuilder.Entity<MoodLog>().HasOne(ml => ml.Mood).WithMany(m => m.MoodLogs).HasForeignKey(ml => ml.MoodId);
            modelBuilder.Entity<MoodLog>().HasOne(ml => ml.User).WithMany(u => u.MoodLogs).HasForeignKey(ml => ml.UserId);

            modelBuilder.Entity<HabitLog>().HasKey(ml => ml.HabitLogId);
            modelBuilder.Entity<HabitLog>().HasOne(ml => ml.Habit).WithMany(m => m.HabitLogs).HasForeignKey(ml => ml.HabitId);
            modelBuilder.Entity<HabitLog>().HasOne(ml => ml.User).WithMany(m => m.HabitLogs).HasForeignKey(ml => ml.UserId);

            modelBuilder.Entity<UserHabit>().HasKey(ml => ml.UserHabitId);
            modelBuilder.Entity<UserHabit>().HasOne(ml => ml.User).WithMany(m => m.UserHabits).HasForeignKey(ml => ml.UserId);
            modelBuilder.Entity<UserHabit>().HasOne(ml => ml.Habit).WithMany(m => m.UserHabits).HasForeignKey(ml => ml.UserHabitId);


        }
    }
}

