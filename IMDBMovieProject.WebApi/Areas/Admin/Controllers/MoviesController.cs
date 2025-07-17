using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.Entities.Entities;
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

    public class MoviesController : Controller
    {
        private readonly DataBaseContext _context;

        public MoviesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Movies
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Movies.Include(m => m.Category);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Admin/Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // GET: Admin/Movies/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Movies movies , IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                movies.Image = await FileHelper.FileLoaderAsync(Image, "/Img/Movies/");
                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", movies.CategoryId);
            return View(movies);
        }

        // GET: Admin/Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", movies.CategoryId);
            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movies movies , IFormFile? Image, bool DeleteImage = false)
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (DeleteImage)
                        movies.Image = string.Empty;
                    if (Image is not null)
                        movies.Image = await FileHelper.FileLoaderAsync(Image);
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", movies.CategoryId);
            return View(movies);
        }

        // GET: Admin/Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Admin/Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movies = await _context.Movies.FindAsync(id);
            if (movies != null)
            {
                _context.Movies.Remove(movies);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
