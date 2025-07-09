using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBMovieProject.Entities.Entities
{
    public class Slider
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Link { get; set; }
    }
}
