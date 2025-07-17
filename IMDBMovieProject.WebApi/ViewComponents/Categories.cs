using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.WebApi.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDBMovieProject.WebApi.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly DataBaseContext _context;


        public Categories(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Categories.ToListAsync());
        }
    }
}
