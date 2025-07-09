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

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Year { get; set; }

        public string Rating { get; set; }

        public string? Image { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsNew { get; set; }
    }
}
