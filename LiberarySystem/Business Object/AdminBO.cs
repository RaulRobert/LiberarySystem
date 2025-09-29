using library_system.Models;
using Login.Models;
using Microsoft.EntityFrameworkCore;

namespace library_system.Business
{
    public class AdminBO
    {
        private readonly AppDbContext _context;

        public AdminBO(AppDbContext context)
        {
            _context = context;
        }

        // Dashboard summary
        public object GetDashboardSummary()
        {
            return new
            {
                UsersCount = _context.Authentication.Count(),
                BooksCount = _context.Book.Count(),
                AuthorsCount = _context.Author.Count(),
                PublishersCount = _context.Pubblisher.Count(),
                TipologiesCount = _context.Tipology.Count()
            };
        }

        // Manage users
        public List<Authentication> GetAllUsers()
        {
            return _context.Authentication.ToList();
        }

        // Manage books
        public List<Book> GetAllBooks()
        {
            return _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Pubblisher)
                .Include(b => b.Tipology)
                .ToList();
        }
    }
}
