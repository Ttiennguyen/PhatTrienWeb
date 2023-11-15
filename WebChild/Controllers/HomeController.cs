using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebChild.Data;
using WebChild.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebChild.Data;

namespace WebChild.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger,AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public  IActionResult Index()
    {
        var appDbContext =   _context.Products.Take(4).Where(p=>p.CategoryId==1).ToList();
        var otherProducts = _context.Products.Take(4).Where(p => p.CategoryId == 2).ToList();
        var toyProduct = _context.Products.Take(4).Where(p => p.CategoryId == 3).ToList();
        // view bag 
        ViewBag.Products = appDbContext;
        ViewBag.OtherProducts = otherProducts;
        ViewBag.ToyProduct = toyProduct;
        return View();
    }

    // public async Task<IActionResult> List(int id)
    // {
    //     var appDbContext = _context.Products.Include(p => p.Category).Include(p => p.Status).Where(p=>p.CategoryId==id);
    //     return View(appDbContext.ToList());
    // }
    
    public IActionResult AboutUs()
    {
        return View();
    }

    public IActionResult ContactUs()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}