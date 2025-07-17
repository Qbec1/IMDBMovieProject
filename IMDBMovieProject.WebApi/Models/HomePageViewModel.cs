using IMDBMovieProject.Entities.Entities;

namespace IMDBMovieProject.WebApi.Models
{
    public class HomePageViewModel
    {
        public List<Slider>? Sliders { get; set; }
        public List<Movies>? Movies { get; set; }
        public List<News>? News { get; set; }
    }
}
