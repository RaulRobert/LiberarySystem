using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_system.Models;

namespace LiberarySystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        private string getPath(string fileName)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return "~/Views/User/Books/" + fileName + ".cshtml";
            }
            return "~/Views/Admin/Books/" + fileName + ".cshtml";
        }

        // GET: Books

        public async Task<IActionResult> Index()
        {
       

            return View(getPath(nameof(Index)), await _context.Book
                    .Include(b => b.Authors)
                    .Include(b => b.Pubblisher)
                    .Include(b => b.Tipology).ToListAsync());
        }
     

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null) return NotFound();
            return View(getPath(nameof(Details)),book);
        }




        // GET: Books/Create (Admin only)
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FirstName");
            ViewData["PubblisherId"] = new SelectList(_context.Set<Pubblisher>(), "Id", "Company");
            ViewData["TipologyId"] = new SelectList(_context.Set<Tipology>(), "Id", "Description");

            return View(getPath(nameof(Create)));
        }

        // POST: Books/Create (Admin only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            {
                _context.Book.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }        

        // GET: Books/Edit/5 (Admin only)
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            if (id == null) return NotFound();

            var book = await _context.Book.FindAsync(id);
            if (book == null) return NotFound();

            return View(getPath(nameof(Edit)));
        }

        // POST: Books/Edit/5 (Admin only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            if (id != book.Id) return NotFound();

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Book.Any(e => e.Id == book.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Delete/5 (Admin only)
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            if (id == null) return NotFound();

            var book = await _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null) return NotFound();

            return View(getPath(nameof(Delete)));
        }

        // POST: Books/Delete/5 (Admin only)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            var book = await _context.Book.FindAsync(id);
            if (book != null) _context.Book.Remove(book);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}


