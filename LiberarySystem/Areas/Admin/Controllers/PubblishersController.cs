using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_system.Models;

namespace LiberarySystem.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class PubblishersController : Controller
    {
        private readonly AppDbContext _context;

        public PubblishersController(AppDbContext context)
        {
            _context = context;
        }

     


        // GET: Pubblishers
        public async Task<IActionResult> Index()
        {
           
            return View( await _context.Pubblisher.ToListAsync());
        }
       


        // GET: Pubblishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pubblisher = await _context.Pubblisher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pubblisher == null)
            {
                return NotFound();
            }

            return View();
        }

        // GET: Pubblishers/Create
        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Company,CreatedAt,UpdatedAt,DeletedAt,CreatedBy")] Pubblisher pubblisher)
        {
          
                _context.Add(pubblisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        
        // GET: Pubblishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pubblisher = await _context.Pubblisher.FindAsync(id);
            if (pubblisher == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Company,CreatedAt,UpdatedAt,DeletedAt,CreatedBy")] Pubblisher pubblisher)
        {
            if (id != pubblisher.Id)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(pubblisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PubblisherExists(pubblisher.Id))
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
      

        // GET: Pubblishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pubblisher = await _context.Pubblisher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pubblisher == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Pubblishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pubblisher = await _context.Pubblisher.FindAsync(id);
            if (pubblisher != null)
            {
                _context.Pubblisher.Remove(pubblisher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PubblisherExists(int id)
        {
            return _context.Pubblisher.Any(e => e.Id == id);
        }
    }
}
