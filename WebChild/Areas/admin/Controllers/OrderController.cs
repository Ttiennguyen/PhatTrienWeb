using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebChild.Data;
using WebChild.Models;

namespace WebChild.Areas.admin.Controllers
{
    [Area("admin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/Order
        public async Task<IActionResult> Index()
        {
              return _context.Orders != null ? 
                          View(await _context.Orders.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Orders'  is null.");
        }
        
        public  async Task<IActionResult> UpdateOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            order.Status= "Đang trên đường giao";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Order","admin");
        }
        
        public  async Task<IActionResult> UpdateSuccess(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            order.Status= "Giao thành công";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Order","admin");
        }

        
        // GET: Order/OrderDetail/5
        public  async Task<IActionResult> OrderDetail(int id)
        {
            var appDbContext = await _context.Product_Orders.Include(p => p.Order).Include(p => p.Product).Where(p=>p.OrderId==id).ToListAsync();
            return View(appDbContext);
        }
        
        // GET: admin/Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'AppDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
