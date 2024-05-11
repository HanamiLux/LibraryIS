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
    public class BookinstancesController : Controller
    {
        private readonly LibraryContext _context;

        public BookinstancesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Bookinstances
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Bookinstances.Include(b => b.Book).Include(b => b.Condition);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Bookinstances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookinstance = await _context.Bookinstances
                .Include(b => b.Book)
                .Include(b => b.Condition)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookinstance == null)
            {
                return NotFound();
            }

            return View(bookinstance);
        }

        // GET: Bookinstances/Create
        public IActionResult Create()
        {
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title");
            ViewData["Conditionid"] = new SelectList(_context.Conditions, "Id", "Name");
            return View();
        }

        // POST: Bookinstances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bookid,Conditionid,Acquireddate,Isavailable")] Bookinstance bookinstance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookinstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title", bookinstance.Bookid);
            ViewData["Conditionid"] = new SelectList(_context.Conditions, "Id", "Name", bookinstance.Conditionid);
            return View(bookinstance);
        }

        // GET: Bookinstances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookinstance = await _context.Bookinstances.FindAsync(id);
            if (bookinstance == null)
            {
                return NotFound();
            }
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title", bookinstance.Bookid);
            ViewData["Conditionid"] = new SelectList(_context.Conditions, "Id", "Name", bookinstance.Conditionid);
            return View(bookinstance);
        }

        // POST: Bookinstances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bookid,Conditionid,Acquireddate,Isavailable")] Bookinstance bookinstance)
        {
            if (id != bookinstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookinstance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookinstanceExists(bookinstance.Id))
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
            ViewData["Bookid"] = new SelectList(_context.Books, "Id", "Title", bookinstance.Bookid);
            ViewData["Conditionid"] = new SelectList(_context.Conditions, "Id", "Name", bookinstance.Conditionid);
            return View(bookinstance);
        }

        // GET: Bookinstances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookinstance = await _context.Bookinstances
                .Include(b => b.Book)
                .Include(b => b.Condition)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookinstance == null)
            {
                return NotFound();
            }

            return View(bookinstance);
        }

        // POST: Bookinstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookinstance = await _context.Bookinstances.FindAsync(id);
            if (bookinstance != null)
            {
                _context.Bookinstances.Remove(bookinstance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookinstanceExists(int id)
        {
            return _context.Bookinstances.Any(e => e.Id == id);
        }
    }
}
