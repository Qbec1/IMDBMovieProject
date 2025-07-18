using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBMovieProject.Entities.Entities
{
    public class ImdbTop100
    {
        public int Id { get; set; }
        [Display(Name = "Filmin Adı")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Konusu")]
        public string Description { get; set; }
        [Display(Name = "Yayınlanma Yılı")]
        public string Year { get; set; }
        [Display(Name = "IMDB Puanı")]
        public string Rating { get; set; }
        [Display(Name = "Kapak Fotoğrafı")]
        public string? Image { get; set; }
        [Display(Name = "Oluşturma Tarihi")]

        public DateTime CreatedDate { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [Display(Name = "Listeye Yeni Gelen Film")]
        public bool IsNew { get; set; }
    }
}
