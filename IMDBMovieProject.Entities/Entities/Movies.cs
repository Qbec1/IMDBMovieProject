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
        [Display(Name = "Filmin Adı")]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "Konusu")]
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Yayınlanma Tarihi")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Yönetmenin İsmi")]
        public string Director { get; set; } = string.Empty;
        [Display(Name = "Türü")]
        public string Genre { get; set; } = string.Empty;
        [Display(Name = "IMDB Puanı")]
        public double Rating { get; set; }
        [Display(Name = "Süresi")]
        public int DurationInMinutes { get; set; }
        [Display(Name = "Kapak Fotoğrafı")]
        public string? Image { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; } = true;
        [Display(Name = "Ana Sayfada Gösterilsin Mi?")]
        public bool IsHome { get; set; } = true;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [Display(Name = "Oluşturma Tarihi"), ScaffoldColumn(false)]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [Display(Name = "Sıra Numarası")]
        public int OrderNo { get; set; }
        public Guid? MovieGuid { get; set; } = Guid.NewGuid();
    }
}
