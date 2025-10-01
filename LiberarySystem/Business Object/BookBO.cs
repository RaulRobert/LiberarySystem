using LiberarySystem.Models;
using library_system.Models;
using Login.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace library_system.Business
{
    public class BookBO
    {


        private readonly AppDbContext _context;

        public BookBO(AppDbContext context)
        {
            _context = context;
        }

        // Get all books with related data
        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = await _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .ToListAsync();

            // set IsBorrowed for UI
            foreach (var book in books)
            {
                book.IsBorrowed = _context.Borrows
                    .Any(b => b.BookId == book.Id && b.ReturnDate == null);
            }

            return books;
        }


        // Get one book by Id with related data
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // Find book (simple find without includes)
        public async Task<Book?> FindAsync(int id)
        {
            return await _context.Book.FindAsync(id);
        }

        // Add book
        public async Task AddBookAsync(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();
        }

        // Update book
        public async Task UpdateBookAsync(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        // Check existence
        public bool Exists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        // Delete book
        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BorrowBookAsync(int bookId, int authenticationId)
        {
            var existing = await _context.Borrows
                .FirstOrDefaultAsync(b => b.BookId == bookId && b.ReturnDate == null);

            if (existing != null)
                return false; // Book already borrowed

            var borrow = new Borrow
            {
                BookId = bookId,
                AuthenticationId = authenticationId,
                BorrowDate = DateTime.Now
            };

            _context.Borrows.Add(borrow);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReturnBookAsync(int bookId, int authenticationId)
        {
            var borrow = await _context.Borrows
                .FirstOrDefaultAsync(b => b.BookId == bookId && b.AuthenticationId == authenticationId && b.ReturnDate == null);

            if (borrow == null)
                return false;

            borrow.ReturnDate = DateTime.Now;
            _context.Borrows.Update(borrow);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Book>> GetAvailableBooksAsync()
        {
            var books = await _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .ToListAsync();

            foreach (var book in books)
            {
                book.IsBorrowed = _context.Borrows
                    .Any(b => b.BookId == book.Id && b.ReturnDate == null);
            }

            // Return only books that are NOT borrowed
            return books.Where(b => !b.IsBorrowed).ToList();
        }

        public async Task<List<Borrow>> GetUserBorrowsAsync(int authenticationId)
        {
            return await _context.Borrows
                .Include(b => b.book)
                    .ThenInclude(bk => bk.Authors)
                .Include(b => b.book)
                    .ThenInclude(bk => bk.Pubblisher)
                .Where(b => b.AuthenticationId == authenticationId)
                .OrderByDescending(b => b.BorrowDate)
                .ToListAsync();
        }

    }
}

    

