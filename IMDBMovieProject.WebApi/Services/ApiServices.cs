using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace IMDBMovieProject.WebApi.Services
{
    public class ApiServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "2ba5687cd2msh360ce3c289116fbp119bcbjsnb3eaeb6b7b8e";
        private readonly string _apiHost = "imdb-top-100-movies.p.rapidapi.com";
        private readonly DataBaseContext _context;

        public ApiServices(DataBaseContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _apiHost);
        }
        public async Task<List<ImdbTop100>> GetTop100MoviesAsync()
        {
            var result = new List<ImdbTop100>();
            var response = await _httpClient.GetAsync("https://imdb-top-100-movies.p.rapidapi.com/");

            if (!response.IsSuccessStatusCode)
            {
                // Optionally log the error or handle specific status codes
                return result;
            }

            var json = await response.Content.ReadAsStringAsync();

            JObject jsonObject;
            try
            {
                jsonObject = JObject.Parse(json);
            }
            catch
            {
                // Invalid JSON, return empty result
                return result;
            }

            var dataToken = jsonObject["movies"];
            if (dataToken == null || dataToken.Type != JTokenType.Array)
            {
                // "movies" property missing or not an array (e.g., quota exceeded)
                return result;
            }

            var data = (JArray)dataToken;

            foreach (var item in data)
            {
                if (double.TryParse(item["rating"]?.ToString(), out double raiting) && raiting >= 8.0)
                {
                    string title = item["title"]?.ToString();

                    bool ImdbTop100Exists = await _context.Top100s.AnyAsync(m => m.Title == title);

                    if (!ImdbTop100Exists)
                    {
                        var ımdbTop100 = new ImdbTop100
                        {
                            Title = item["title"]?.ToString(),
                            Description = item["description"]?.ToString(),
                            Year = item["year"]?.ToString(),
                            Rating = item["rating"]?.ToString(),
                            Image = item["image"]?.ToString(),
                            CreatedDate = DateTime.Now,
                            IsNew = true
                        };

                        _context.Top100s.Add(ımdbTop100);
                        result.Add(ımdbTop100);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return result;
        }
    }
}
