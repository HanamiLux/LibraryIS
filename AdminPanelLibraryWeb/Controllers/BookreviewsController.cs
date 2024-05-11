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
    public class BookreviewsController : Controller
    {
        private readonly LibraryContext _context;

        public BookreviewsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Bookreviews
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Bookreviews.Include(b => b.Book).Include(b => b.User);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Bookreviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookreview = await _context.Bookreviews
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookreview == null)
            {
                return NotFound();
            }

            return View(bookreview);
        }

        // GET: Bookreviews/Create
        public IActionResult Create()
        {
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title");
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Login");
            return View();
        }

        // POST: Bookreviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bookid,Userid,Reviewcontent,Rate,Reviewdate")] Bookreview bookreview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookreview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title", bookreview.Bookid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Login", bookreview.Userid);
            return View(bookreview);
        }

        // GET: Bookreviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookreview = await _context.Bookreviews.FindAsync(id);
            if (bookreview == null)
            {
                return NotFound();
            }
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title", bookreview.Bookid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Login", bookreview.Userid);
            return View(bookreview);
        }

        // POST: Bookreviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bookid,Userid,Reviewcontent,Rate,Reviewdate")] Bookreview bookreview)
        {
            if (id != bookreview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookreview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookreviewExists(bookreview.Id))
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
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title", bookreview.Bookid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Login", bookreview.Userid);
            return View(bookreview);
        }

        // GET: Bookreviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookreview = await _context.Bookreviews
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookreview == null)
            {
                return NotFound();
            }

            return View(bookreview);
        }

        // POST: Bookreviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookreview = await _context.Bookreviews.FindAsync(id);
            if (bookreview != null)
            {
                _context.Bookreviews.Remove(bookreview);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookreviewExists(int id)
        {
            return _context.Bookreviews.Any(e => e.Id == id);
        }
    }
}
