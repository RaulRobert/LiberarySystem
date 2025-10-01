 using library_system.Business;
using library_system.Models;
using Login.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LiberarySystem.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class BooksController : Controller
    {
        private readonly BookBO _bookBO;
        private readonly AuthorBO _authorBO;
        private readonly TipologyBO _tipologyBO;
        private readonly PubblisherBO  _pubblisherBO ;
        public BooksController(BookBO bookBO, AuthorBO authorBO, TipologyBO tipologyBO, PubblisherBO pubblisherBO)
        {
            _bookBO = bookBO;
            _authorBO = authorBO;
            _tipologyBO = tipologyBO;
            _pubblisherBO = pubblisherBO;
        }

     

        public async Task<IActionResult> Index()
        {
            var path = Request.QueryString;
            var books = await _bookBO.GetAllBooksAsync();
            return View(books);
        }

        //[Route("/User/Books/Index")]
        //public async Task<IActionResult> IndexUser()
        //{
        //    var books = await _bookBO.GetAllBooksAsync();
        //    return View(getPath(nameof(Index)), books);
        //}


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _bookBO.GetBookByIdAsync(id.Value);
            if (book == null) return NotFound();

            return View(book);
        }

        // GET: Books/Create (Admin only)
        public IActionResult Create()
        {
           

            ViewData["AuthorId"] = _authorBO.GetAuthorsSelectList();
            ViewData["PubblisherId"] = _pubblisherBO.GetPubblisherSelectList();
            ViewData["TipologyId"] = _tipologyBO.GetTipologySelectList();

            return View();
        }

        // POST: Books/Create (Admin only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            await _bookBO.AddBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(int id)
        {
            int authenticationId = 1; // TODO: replace with logged-in user
            var success = await _bookBO.BorrowBookAsync(id, authenticationId);

            if (!success)
                TempData["Error"] = "Book is already borrowed.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            int authenticationId = 1; // TODO: replace with logged-in user
            var success = await _bookBO.ReturnBookAsync(id, authenticationId);

            if (!success)
                TempData["Error"] = "Cannot return this book.";

            return RedirectToAction(nameof(Index));
        }


        // GET: Books/Edit/5 (Admin only)
        public async Task<IActionResult> Edit(int? id)
        { 
            if (id == null) return NotFound();

            var book = await _bookBO.FindAsync(id.Value);
            if (book == null) return NotFound();

            ViewData["AuthorId"] = _authorBO.GetAuthorsSelectList();
            ViewData["PubblisherId"] = _pubblisherBO.GetPubblisherSelectList();
            ViewData["TipologyId"] = _tipologyBO.GetTipologySelectList();

            return View(book);
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
                await _bookBO.UpdateBookAsync(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_bookBO.Exists(book.Id))
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

            var book = await _bookBO.GetBookByIdAsync(id.Value);
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Books/Delete/5 (Admin only)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            await _bookBO.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
