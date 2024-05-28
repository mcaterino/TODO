using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoAPI.Models;

namespace ToDoAPI.Data
{
    public class AppDbContext : DbContext
    {
        private readonly DbSettings _dbSettings;

        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<DbSettings> dbSettings)
            : base(options)
        {
            _dbSettings = dbSettings.Value;
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }

        // Configuring the model for the Todo entity
         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<Todo>()
                 .ToTable("TodoAPI")
                 .HasKey(x => x.Id);
         }
    }
}