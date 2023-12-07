using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebChild.Data;

namespace WebChild.Areas.admin.Controllers
{
    [Area("admin")]
    public class StatusController : Controller
    {
        private readonly AppDbContext _context;

        public StatusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/Status
        public async Task<IActionResult> Index()
        {
              return _context.StatusEnumerable != null ? 
                          View(await _context.StatusEnumerable.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.StatusEnumerable'  is null.");
        }

        // GET: admin/Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StatusEnumerable == null)
            {
                return NotFound();
            }

            var status = await _context.StatusEnumerable
                .FirstOrDefaultAsync(m => m.id == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: admin/Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,StatusName")] Status status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(status);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: admin/Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StatusEnumerable == null)
            {
                return NotFound();
            }

            var status = await _context.StatusEnumerable.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: admin/Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,StatusName")] Status status)
        {
            if (id != status.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(status);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.id))
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
            return View(status);
        }

        // GET: admin/Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StatusEnumerable == null)
            {
                return NotFound();
            }

            var status = await _context.StatusEnumerable
                .FirstOrDefaultAsync(m => m.id == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: admin/Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StatusEnumerable == null)
            {
                return Problem("Entity set 'AppDbContext.StatusEnumerable'  is null.");
            }
            var status = await _context.StatusEnumerable.FindAsync(id);
            if (status != null)
            {
                _context.StatusEnumerable.Remove(status);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(int id)
        {
          return (_context.StatusEnumerable?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
