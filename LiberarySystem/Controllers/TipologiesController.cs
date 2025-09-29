using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library_system.Models;

namespace LiberarySystem.Controllers
{
    public class TipologiesController : Controller
    {
        private readonly AppDbContext _context;

        public TipologiesController(AppDbContext context)
        {
            _context = context;
        }
        private string getPath(string fileName)
        {
            return "~/Views/Admin/Tipologies/" + fileName + ".cshtml";
        }

        // GET: Tipologies
        public async Task<IActionResult> Index()
        {
            return View(getPath(nameof(Index)),await _context.Tipology.ToListAsync());
        }

        // GET: Tipologies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipology = await _context.Tipology
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipology == null)
            {
                return NotFound();
            }

            return View(getPath(nameof(Details)));
        }

        // GET: Tipologies/Create
        public IActionResult Create()
        {
            return View(getPath(nameof(Create)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,CreatedAt,UpdatedAt,DeletedAt,CreatedBy")] Tipology tipology)
        {
   
                _context.Add(tipology);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
     

        // GET: Tipologies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipology = await _context.Tipology.FindAsync(id);
            if (tipology == null)
            {
                return NotFound();
            }
            return View(getPath(nameof(Edit)));
        }

        // POST: Tipologies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,CreatedAt,UpdatedAt,DeletedAt,CreatedBy")] Tipology tipology)
        {
            if (id != tipology.Id)
            {
                return NotFound();
            }

        
                try
                {
                    _context.Update(tipology);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipologyExists(tipology.Id))
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
       

        // GET: Tipologies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipology = await _context.Tipology
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipology == null)
            {
                return NotFound();
            }

            return View(getPath(nameof(Delete)));
        }

        // POST: Tipologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipology = await _context.Tipology.FindAsync(id);
            if (tipology != null)
            {
                _context.Tipology.Remove(tipology);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipologyExists(int id)
        {
            return _context.Tipology.Any(e => e.Id == id);
        }
    }
}
