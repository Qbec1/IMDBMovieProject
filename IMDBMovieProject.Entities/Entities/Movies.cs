using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBMovieProject.Entities.Entities
{
    public sealed class Movies : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int DurationInMinutes { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; } = true;
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public Guid? MovieGuid { get; set; } = Guid.NewGuid();
    }
}
