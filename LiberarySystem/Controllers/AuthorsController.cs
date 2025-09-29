using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using library_system.Models;

namespace LiberarySystem.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        private string getPath(string fileName)
        {
            return "~/Views/Admin/Authors/" + fileName + ".cshtml";
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(getPath(nameof(Index)), await _context.Author.ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var author = await _context.Author.FirstOrDefaultAsync(m => m.Id == id);
            if (author == null) return NotFound();

            return View(author);
        }

        // GET: Authors/Create (Admin only)
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            return View(getPath(nameof(Create)));
        }


        // POST: Authors/Create (Admin only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            _context.Add(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Edit/5 (Admin only)
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            if (id == null) return NotFound();

            var author = await _context.Author.FindAsync(id);
            if (author == null) return NotFound();

            return View(getPath(nameof(Edit)));
        }

        // POST: Authors/Edit/5 (Admin only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            if (id != author.Id) return NotFound();

            try
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Author.Any(e => e.Id == author.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Delete/5 (Admin only)
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            if (id == null) return NotFound();

            var author = await _context.Author.FirstOrDefaultAsync(m => m.Id == id);
            if (author == null) return NotFound();

            return View(getPath(nameof(Delete)));
        }

        // POST: Authors/Delete/5 (Admin only)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            var author = await _context.Author.FindAsync(id);
            if (author != null) _context.Author.Remove(author);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
