using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Json;


namespace IMDBMovieProject.DataAccess.Data
{
    public class DataBaseContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
    {
        public DataBaseContext CreateDbContext(string[] args)
        {

            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new DataBaseContext(optionsBuilder.Options);
        }
    }
}
