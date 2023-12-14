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
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger,AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

  
        // GET: Order/OrderDetail/5
        public  async Task<IActionResult> OrderDetail(int id)
        {
            var appDbContext = await _context.Product_Orders.Include(p => p.Order).Include(p => p.Product).Where(p=>p.OrderId==id).ToListAsync();
            return View(appDbContext);
        }
        
        public async Task<IActionResult> OrderUser(string name)
        {
            var appDbContext = await _context.Orders.Where(p=>p.Email_User==name).ToListAsync();
            return View(appDbContext);
        }
        
        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
