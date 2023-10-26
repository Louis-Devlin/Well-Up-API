using Microsoft.EntityFrameworkCore;

namespace Well_Up_API.Models
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }
        public DbSet<TestModel> Test { get; set; }

        public DbSet<Mood> Mood { get; set; }

        
    }
}

