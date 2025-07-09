using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMDBMovieProject.DataAccess;
using IMDBMovieProject.Entities.Entities;

namespace IMDBMovieProject.WebApi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImdbTop100Controller : Controller
    {
        private readonly DataBaseContext _context;

        public ImdbTop100Controller(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Admin/ImdbTop100
        public async Task<IActionResult> Index()
        {
            return View(await _context.Top100s.ToListAsync());
        }

        // GET: Admin/ImdbTop100/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imdbTop100 = await _context.Top100s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imdbTop100 == null)
            {
                return NotFound();
            }

            return View(imdbTop100);
        }

        // GET: Admin/ImdbTop100/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ImdbTop100/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Year,Rating,Image,CreatedDate,IsNew")] ImdbTop100 imdbTop100)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imdbTop100);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imdbTop100);
        }

        // GET: Admin/ImdbTop100/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imdbTop100 = await _context.Top100s.FindAsync(id);
            if (imdbTop100 == null)
            {
                return NotFound();
            }
            return View(imdbTop100);
        }

        // POST: Admin/ImdbTop100/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Year,Rating,Image,CreatedDate,IsNew")] ImdbTop100 imdbTop100)
        {
            if (id != imdbTop100.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imdbTop100);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImdbTop100Exists(imdbTop100.Id))
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
            return View(imdbTop100);
        }

        // GET: Admin/ImdbTop100/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imdbTop100 = await _context.Top100s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imdbTop100 == null)
            {
                return NotFound();
            }

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
