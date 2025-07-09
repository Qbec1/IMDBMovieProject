using Microsoft.AspNetCore.Mvc;

namespace IMDBMovieProject.WebApi.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
