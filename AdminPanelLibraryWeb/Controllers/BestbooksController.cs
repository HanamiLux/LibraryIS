using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.Data;
using LibraryWebApi.Models;

namespace AdminPanelLibraryWeb.Controllers
{
    public class BestbooksController : Controller
    {
        private readonly LibraryContext _context;

        public BestbooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Bestbooks
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Bestbooks.Include(b => b.Book);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Bestbooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestbook = await _context.Bestbooks
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestbook == null)
            {
                return NotFound();
            }

            return View(bestbook);
        }

        // GET: Bestbooks/Create
        public IActionResult Create()
        {
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title");
            return View();
        }

        // POST: Bestbooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bookid,Ranking,Year")] Bestbook bestbook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bestbook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title", bestbook.Bookid);
            return View(bestbook);
        }

        // GET: Bestbooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestbook = await _context.Bestbooks.FindAsync(id);
            if (bestbook == null)
            {
                return NotFound();
            }
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title", bestbook.Bookid);
            return View(bestbook);
        }

        // POST: Bestbooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bookid,Ranking,Year")] Bestbook bestbook)
        {
            if (id != bestbook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestbook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestbookExists(bestbook.Id))
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
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title", bestbook.Bookid);
            return View(bestbook);
        }

        // GET: Bestbooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestbook = await _context.Bestbooks
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestbook == null)
            {
                return NotFound();
            }

            return View(bestbook);
        }

        // POST: Bestbooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestbook = await _context.Bestbooks.FindAsync(id);
            if (bestbook != null)
            {
                _context.Bestbooks.Remove(bestbook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestbookExists(int id)
        {
            return _context.Bestbooks.Any(e => e.Id == id);
        }
    }
}
