using library_system.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace library_system.Business
{
    public class AuthorBO
    {
        private readonly AppDbContext _context;

        public AuthorBO(AppDbContext context)
        {
            _context = context;
        }

        // Get all authors
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _context.Author.ToListAsync();
        }

        // Get author by id
        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Author.FindAsync(id);
        }

        // Add a new author
        public async Task AddAuthorAsync(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();
        }

        // Update an existing author
        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Author.Update(author);
            await _context.SaveChangesAsync();
        }

        // Delete an author
        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author != null)
            {
                _context.Author.Remove(author);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Author?> GetForDeleteAsync(int id)
        {
            return await _context.Author.FirstOrDefaultAsync(m => m.Id == id);
        }


        public async Task<Author?> FindAsync(int id)
        {
            return await _context.Author.FindAsync(id);
        }

        public bool Exists(int id)
        {
            return _context.Author.Any(e => e.Id == id);
        }


        public SelectList GetAuthorsSelectList()
        {
            return new SelectList(_context.Set<Author>(), "Id", "FirstName");
        }

    }
}
