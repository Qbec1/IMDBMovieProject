using IMDBMovieProject.Entities;
using IMDBMovieProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Reflection;

namespace IMDBMovieProject.DataAccess.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ImdbTop100> Top100s { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tüm entity'lerde CreatedDate veya CreateDate property'lerine varsayılan değer ata
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var createdDateProperty = entityType.FindProperty("CreatedDate") ?? entityType.FindProperty("CreateDate");
                if (createdDateProperty != null && createdDateProperty.ClrType == typeof(DateTime))
                {
                    createdDateProperty.SetDefaultValueSql("GETDATE()");
                }
            }

            base.OnModelCreating(modelBuilder);
        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=BERKANAYDOGAN\\SQLEXPRESS;Initial Catalog=ProjectMovieDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore
        //     (RelationalEventId.PendingModelChangesWarning)); //Model değişiklikleri için uyarıları yok sayar.

        //    base.OnConfiguring(optionsBuilder);
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //çalışan dll in içinden

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
