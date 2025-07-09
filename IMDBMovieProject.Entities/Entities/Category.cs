using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBMovieProject.Entities.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Descripton { get; set; }
        [Display(Name = "Resim")]
        public string? Image { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Üst Menüde Göster")]
        public bool IsTopMenu { get; set; }
        [Display(Name = "Üst Kategori")]
        public int ParentId { get; set; }
        [Display(Name = "Sıra No")]
        public int OrderNo { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public IList<Movies>? Movies { get; set; }
    }
}
