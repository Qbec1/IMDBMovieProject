using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBMovieProject.Entities.Entities
{
    public sealed class News : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Name  { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        public string? Image { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        public int? CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public Category? Category { get; set; }

        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
