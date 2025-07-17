using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBMovieProject.Entities.Entities
{
    public class Favorite : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; } // Kullanıcı ID
        public int MovieId { get; set; } // Film ID

        public AppUser User { get; set; }
        public Movies Movie { get; set; }




    }
}
