using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Lab1._SimpleApp
{
    public class AppContext : DbContext
    {
        public DbSet<Course> Courses => Set<Course>();
        public AppContext() : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }
    }
}
