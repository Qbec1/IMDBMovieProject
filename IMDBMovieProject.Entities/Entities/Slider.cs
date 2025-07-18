using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBMovieProject.Entities.Entities
{
    public class Slider
    {
        public int Id { get; set; }
        [Display(Name = "Fotoğrafı")]
        public string? Image { get; set; }
        [Display(Name = "Başlığı")]
        public string Title { get; set; }
        [Display(Name = "Tanımı")]
        public string Description { get; set; }
        
        public string? Link { get; set; }
    }
}
