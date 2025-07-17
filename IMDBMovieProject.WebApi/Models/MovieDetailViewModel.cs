using IMDBMovieProject.Entities.Entities;

namespace IMDBMovieProject.WebApi.Models
{
    public class MovieDetailViewModel
    {
        public Movies? Movie { get; set; }
        public IEnumerable<Movies>? RelatedMovies { get; set; }
    }
}
