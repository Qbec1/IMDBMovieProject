using IMDBMovieProject.Entities;
using IMDBMovieProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace IMDBMovieProject.DataAccess
{
    public class DataBaseContext :DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ImdbTop100> Top100s { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=BERKANAYDOGAN\\SQLEXPRESS;Initial Catalog=ProjectMovieDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore
             (RelationalEventId.PendingModelChangesWarning)); //Model değişiklikleri için uyarıları yok sayar.

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //çalışan dll in içinden

            base.OnModelCreating(modelBuilder);
        }
    }
}
