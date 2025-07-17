using IMDBMovieProject.Entities.Entities;

namespace IMDBMovieProject.WebApi.Models
{
    public class NewsDetailViewModel
    {
        public News? News { get; set; }
        public IEnumerable<News>? RelatedNews { get; set; }
    }
}
