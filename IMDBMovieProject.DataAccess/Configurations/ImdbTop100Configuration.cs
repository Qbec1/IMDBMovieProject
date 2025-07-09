using IMDBMovieProject.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBMovieProject.DataAccess.Configurations
{
    public class ImdbTop100Configuration
    {
        public void Configure(EntityTypeBuilder<ImdbTop100> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(50);
            builder.HasData();
        }
    }
}
