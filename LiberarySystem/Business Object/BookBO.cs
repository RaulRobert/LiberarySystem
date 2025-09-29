using library_system.Models;
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

        // Get all books
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .ToListAsync();
        }

        // Get book by id
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        // Create book
        public async Task CreateBookAsync(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
        }

        // Update book
        public async Task UpdateBookAsync(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
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

        // Check if book exists
        public async Task<bool> BookExistsAsync(int id)
        {
            return await _context.Book.AnyAsync(e => e.Id == id);
        }
    }
}
