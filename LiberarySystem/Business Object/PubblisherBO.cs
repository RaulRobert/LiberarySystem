using library_system.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace library_system.Business
{
    public class PubblisherBO
    {
        private readonly AppDbContext _context;

        public PubblisherBO(AppDbContext context)
        {
            _context = context;
        }

        // Get all
        public async Task<List<Pubblisher>> GetAllAsync()
        {
            return await _context.Pubblisher.ToListAsync();
        }

        // Get by id
        public async Task<Pubblisher?> GetByIdAsync(int id)
        {
            return await _context.Pubblisher.FirstOrDefaultAsync(m => m.Id == id);
        }

        // Add
        public async Task AddAsync(Pubblisher pubblisher)
        {
            _context.Add(pubblisher);
            await _context.SaveChangesAsync();
        }

        // Update
        public async Task UpdateAsync(Pubblisher pubblisher)
        {
            _context.Update(pubblisher);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            var pubblisher = await _context.Pubblisher.FindAsync(id);
            if (pubblisher != null)
            {
                _context.Pubblisher.Remove(pubblisher);
                await _context.SaveChangesAsync();
            }
        }

        // Exists
        public bool Exists(int id)
        {
            return _context.Pubblisher.Any(e => e.Id == id);
        }

        
        public SelectList GetPubblisherSelectList()
        {
            return new SelectList(_context.Pubblisher, "Id", "Company");
        }
    }
}
