using IMDBMovieProject.Entities;
using IMDBMovieProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDBMovieProject.DataAccess.Configurations
{
    public class MoviesConfiguration : IEntityTypeConfiguration<Movies>
    {
        public void Configure(EntityTypeBuilder<Movies> builder)
        {
           
            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("varchar(100)").HasMaxLength(100);
           
            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("nvarchar(max)");
            
            builder.Property(x => x.Director)
                .IsRequired()
                .HasColumnType("varchar(50)").HasMaxLength(50);
            
            builder.Property(x => x.Genre)
                .IsRequired()
                .HasColumnType("varchar(50)").HasMaxLength(50);
            
            builder.Property(x => x.Rating)
                .IsRequired()
                .HasColumnType("decimal(3,2)");
           
            builder.Property(x => x.DurationInMinutes)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
