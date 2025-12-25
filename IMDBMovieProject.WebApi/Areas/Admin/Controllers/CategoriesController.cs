using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBMovieProject.WebApi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class CategoriesController : Controller
    {
        private readonly DataBaseContext _context;

        public CategoriesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            ViewBag.Kategoriler = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Kategori başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Kategoriler = new SelectList(_context.Categories, "Id", "Name");
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            ViewBag.Kategoriler = new SelectList(_context.Categories, "Id", "Name");
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(category);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Kategori başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Kategoriler = new SelectList(_context.Categories, "Id", "Name");
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
                return NotFound();

            // 🔒 Bu kategoriye bağlı haber var mı?
            bool hasNews = await _context.News.AnyAsync(n => n.CategoryId == id);
            ViewBag.HasNews = hasNews;

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // 🔒 Güvenlik için tekrar kontrol
            bool hasNews = await _context.News.AnyAsync(n => n.CategoryId == id);

            if (hasNews)
            {
                TempData["ErrorMessage"] =
                    "Bu kategoriye bağlı haberler bulunduğu için silme işlemi gerçekleştirilemedi.";
                return RedirectToAction(nameof(Index));
            }

            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
