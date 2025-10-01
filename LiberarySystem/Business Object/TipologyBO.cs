using library_system.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace library_system.Business
{
    public class TipologyBO
    {
        private readonly AppDbContext _context;

        public TipologyBO(AppDbContext context)
        {
            _context = context;
        }

        // Get all tipologies
        public async Task<List<Tipology>> GetAllAsync()
        {
            return await _context.Tipology.ToListAsync();
        }

        // Get by id
        public async Task<Tipology?> GetByIdAsync(int id)
        {
            return await _context.Tipology.FirstOrDefaultAsync(m => m.Id == id);
        }

        // Add new
        public async Task AddAsync(Tipology tipology)
        {
            _context.Add(tipology);
            await _context.SaveChangesAsync();
        }

        // Update
        public async Task UpdateAsync(Tipology tipology)
        {
            _context.Update(tipology);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            var tipology = await _context.Tipology.FindAsync(id);
            if (tipology != null)
            {
                _context.Tipology.Remove(tipology);
                await _context.SaveChangesAsync();
            }
        }

        // Exists
        public bool Exists(int id)
        {
            return _context.Tipology.Any(e => e.Id == id);
        }

        // ✅ For dropdowns
        public SelectList GetTipologySelectList()
        {
            return new SelectList(_context.Tipology, "Id", "Description");
        }
    }
}
