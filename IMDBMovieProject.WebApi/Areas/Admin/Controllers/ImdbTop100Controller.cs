using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.Entities.Entities;
using IMDBMovieProject.WebApi.Services;
using IMDBMovieProject.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBMovieProject.WebApi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]

    public class ImdbTop100Controller : Controller
    {
        private readonly DataBaseContext _context;
        private readonly ApiServices _apiService;

        public ImdbTop100Controller(DataBaseContext context, ApiServices apiServices)
        {
            _context = context;
            _apiService = apiServices;
        }

        // GET: Admin/ImdbTop100
        public async Task<IActionResult> Index()
        {
            return View(await _context.Top100s.ToListAsync());
        }

        // ✅ GET: Admin/ImdbTop100/New - Yeni filmleri göster
        public IActionResult New()
        {
            var newMovies = _context.Top100s
                .Where(m => m.IsNew)
                .OrderByDescending(m => m.CreatedDate)
                .ToList();

            return View(newMovies);
        }

        // ✅ GET: Admin/ImdbTop100/Import - API'den verileri çekip veritabanına kaydet
        public async Task<IActionResult> Import()
        {
            var moviesFromApi = await _apiService.GetTop100MoviesAsync();

            foreach (var imdbTop100 in moviesFromApi)
            {
                if (!_context.Top100s.Any(m => m.Title == imdbTop100.Title))
                {
                    _context.Top100s.Add(imdbTop100);
                }

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ImdbTop100/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var imdbTop100 = await _context.Top100s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imdbTop100 == null) return NotFound();

            return View(imdbTop100);
        }

        // GET: Admin/ImdbTop100/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ImdbTop100/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ImdbTop100 imdbTop100 , IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                imdbTop100.Image = await FileHelper.FileLoaderAsync(Image);
                _context.Add(imdbTop100);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imdbTop100);
        }

        // GET: Admin/ImdbTop100/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var imdbTop100 = await _context.Top100s.FindAsync(id);
            if (imdbTop100 == null) return NotFound();

            return View(imdbTop100);
        }

        // POST: Admin/ImdbTop100/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ImdbTop100 imdbTop100, IFormFile? Image, bool DeleteImage = false)
        {
            if (id != imdbTop100.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (DeleteImage)
                        imdbTop100.Image = string.Empty;
                    if (Image is not null)
                        imdbTop100.Image = await FileHelper.FileLoaderAsync(Image);
                    _context.Update(imdbTop100);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImdbTop100Exists(imdbTop100.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(imdbTop100);
        }

        // GET: Admin/ImdbTop100/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var imdbTop100 = await _context.Top100s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imdbTop100 == null) return NotFound();

            return View(imdbTop100);
        }

        // POST: Admin/ImdbTop100/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imdbTop100 = await _context.Top100s.FindAsync(id);
            if (imdbTop100 != null)
            {
                if (!string.IsNullOrEmpty(imdbTop100.Image))
                {
                    FileHelper.FileRemover(imdbTop100.Image);
                }
                _context.Top100s.Remove(imdbTop100);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImdbTop100Exists(int id)
        {
            return _context.Top100s.Any(e => e.Id == id);
        }
    }
}