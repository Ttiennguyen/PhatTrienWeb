using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebChild.Data;
using WebChild.Models;

namespace WebChild.Controllers
{
    public class ProductOrderController : Controller
    {
        private readonly AppDbContext _context;

        public ProductOrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductOrder
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Product_Orders.Include(p => p.Order).Include(p => p.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product_Orders == null)
            {
                return NotFound();
            }

            var product_Order = await _context.Product_Orders
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product_Order == null)
            {
                return NotFound();
            }

            return View(product_Order);
        }

        // GET: ProductOrder/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: ProductOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,OrderId,Quanlity,Price")] Product_Order product_Order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product_Order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", product_Order.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", product_Order.ProductId);
            return View(product_Order);
        }

        // GET: ProductOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product_Orders == null)
            {
                return NotFound();
            }

            var product_Order = await _context.Product_Orders.FindAsync(id);
            if (product_Order == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", product_Order.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", product_Order.ProductId);
            return View(product_Order);
        }

        // POST: ProductOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,OrderId,Quanlity,Price")] Product_Order product_Order)
        {
            if (id != product_Order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product_Order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product_OrderExists(product_Order.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", product_Order.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", product_Order.ProductId);
            return View(product_Order);
        }

        // GET: ProductOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product_Orders == null)
            {
                return NotFound();
            }

            var product_Order = await _context.Product_Orders
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product_Order == null)
            {
                return NotFound();
            }

            return View(product_Order);
        }

        // POST: ProductOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product_Orders == null)
            {
                return Problem("Entity set 'AppDbContext.Product_Orders'  is null.");
            }
            var product_Order = await _context.Product_Orders.FindAsync(id);
            if (product_Order != null)
            {
                _context.Product_Orders.Remove(product_Order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product_OrderExists(int id)
        {
          return (_context.Product_Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
