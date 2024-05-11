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
    public class BookrentalsController : Controller
    {
        private readonly LibraryContext _context;

        public BookrentalsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Bookrentals
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Bookrentals.Include(b => b.Instance).Include(b => b.User);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Bookrentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookrental = await _context.Bookrentals
                .Include(b => b.Instance)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookrental == null)
            {
                return NotFound();
            }

            return View(bookrental);
        }

        // GET: Bookrentals/Create
        public IActionResult Create()
        {
            ViewData["Instanceid"] = new SelectList(_context.Bookinstances, "Id", "Id");
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Login");
            return View();
        }

        // POST: Bookrentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Userid,Instanceid,Daterented,Datereturned,Price")] Bookrental bookrental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookrental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Instanceid"] = new SelectList(_context.Bookinstances, "Id", "Id", bookrental.Instanceid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Login", bookrental.Userid);
            return View(bookrental);
        }

        // GET: Bookrentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookrental = await _context.Bookrentals.FindAsync(id);
            if (bookrental == null)
            {
                return NotFound();
            }
            ViewData["Instanceid"] = new SelectList(_context.Bookinstances, "Id", "Id", bookrental.Instanceid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Login", bookrental.Userid);
            return View(bookrental);
        }

        // POST: Bookrentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Userid,Instanceid,Daterented,Datereturned,Price")] Bookrental bookrental)
        {
            if (id != bookrental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookrental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookrentalExists(bookrental.Id))
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
            ViewData["Instanceid"] = new SelectList(_context.Bookinstances, "Id", "Id", bookrental.Instanceid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Login", bookrental.Userid);
            return View(bookrental);
        }

        // GET: Bookrentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookrental = await _context.Bookrentals
                .Include(b => b.Instance)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookrental == null)
            {
                return NotFound();
            }

            return View(bookrental);
        }

        // POST: Bookrentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookrental = await _context.Bookrentals.FindAsync(id);
            if (bookrental != null)
            {
                _context.Bookrentals.Remove(bookrental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookrentalExists(int id)
        {
            return _context.Bookrentals.Any(e => e.Id == id);
        }
    }
}
