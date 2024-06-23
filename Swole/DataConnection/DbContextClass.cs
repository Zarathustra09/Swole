using Swole.Model;
using Swole.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Swole.DataConnection
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(DbContextOptions<DbContextClass> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Login>().HasNoKey(); // Configure Login as a keyless entity
        }

        public DbSet<User> Users { get; set; }
 
        public DbSet<Login> Logins { get; set; }

        public DbSet<ExerciseCategory> ExerciseCategories { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseRecord> ExerciseRecords { get; set; }
    }
}
