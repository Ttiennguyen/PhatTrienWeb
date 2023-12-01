using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebChild.Data;
using WebChild.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


namespace WebChild.Controllers
{
    public class ProductController : Controller
    {
        
        
        private readonly AppDbContext _context;
        private readonly ILogger<ProductController> _logger;
        
        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";
        
        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems () {

            var session = HttpContext.Session;
            string jsoncart = session.GetString (CARTKEY);
            if (jsoncart != null) {
                return JsonConvert.DeserializeObject<List<CartItem>> (jsoncart);
            }
            return new List<CartItem> ();
        }
        
        // Xóa cart khỏi session
        void ClearCart () {
            var session = HttpContext.Session;
            session.Remove (CARTKEY);
        }
        
        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession (List<CartItem> ls) {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject (ls);
            session.SetString (CARTKEY, jsoncart);
        }

        public ProductController(ILogger<ProductController> logger,AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        // GET: Product
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        
        public async Task<IActionResult> Category(int id)
        {
            var appDbContext = _context.Products.Include(p => p.Category).Include(p => p.Status).Where(p=>p.CategoryId==id);
            return View(await appDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> List(int id)
        {
            var appDbContext = _context.Products.Include(p => p.Category).Include(p => p.Status).Where(p=>p.CategoryId==id);
            return View(await appDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> DetailProduct(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> DetailProductSale(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "id", "CategoryName");
            ViewData["StatusId"] = new SelectList(_context.StatusEnumerable, "id", "StatusName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,ProductName,ProductPrice,ProductPriceSale,ProductAmount,DateInclude,Brand,image,StatusId,ProductDescription")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "id", "CategoryName", product.CategoryId);
            ViewData["StatusId"] = new SelectList(_context.StatusEnumerable, "id", "StatusName", product.StatusId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "id", "CategoryName", product.CategoryId);
            ViewData["StatusId"] = new SelectList(_context.StatusEnumerable, "id", "StatusName", product.StatusId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,ProductName,ProductPrice,ProductPriceSale,ProductAmount,DateInclude,Brand,image,StatusId,ProductDescription")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "id", "CategoryName", product.CategoryId);
            ViewData["StatusId"] = new SelectList(_context.StatusEnumerable, "id", "StatusName", product.StatusId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        /* ******************************************************** */
        
        /// Thêm sản phẩm vào cart
        [Route ("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute]int productid) {
            var product = _context.Products
                .Where (p => p.Id == productid)
                .FirstOrDefault ();
            if (product == null)
                return NotFound ("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.product.Id == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            } else {
                //  Thêm mới
                cart.Add (new CartItem () { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession (cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction (nameof (Cart));
        }
        
        /// xóa item trong cart
        [Route ("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart ([FromRoute] int productid) {
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.product.Id == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession (cart);
            return RedirectToAction (nameof (Cart));
        }
        
        /// Cập nhật
        [Route ("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart ([FromForm] int productid, [FromForm] int quantity) {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.product.Id == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession (cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        [Route("/checkout")]
        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng
            return View();
        }

        /* ******************************************************** */
        private List<Product> ShoppingCart
        {
            get
            {
                var cartJson = HttpContext.Session.GetString("ShoppingCart");
                return string.IsNullOrEmpty(cartJson) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(cartJson);
            }
            set
            {
                HttpContext.Session.SetString("ShoppingCart", JsonConvert.SerializeObject(value));
            }
        }
        // Add item to the shopping cart
        public IActionResult AddToCart(int? id)
        {

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            var cart = ShoppingCart;
            cart.Add(product);
            ShoppingCart = cart;

            return RedirectToAction(nameof(ShoppingCartView));
        }
        // Display shopping cart
        public IActionResult ShoppingCartView()
        {
            var cart = ShoppingCart;
            return View(cart);
        }
        // Remove item from the shopping cart
        public IActionResult RemoveFromCart(int? id)
        {
            //     var product = _context.Products.FirstOrDefault(p => p.Id == id);
            //
            //     var cart = ShoppingCart;
            //     cart.Remove(product);
            //     ShoppingCart = cart;
            //
            //     return RedirectToAction(nameof(ShoppingCartView));
            // Check if the id is valid
            if (id == null)
            {
                return NotFound();
            }

            // Log debug information
            Console.WriteLine($"Attempting to remove product with id: {id}");

            // Find the product with the specified id
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            // Check if the product is found
            if (product == null)
            {
                Console.WriteLine($"Product with id {id} not found");
                return NotFound();
            }

            // Log debug information
            Console.WriteLine($"Product found: {product.ProductName}");
            
            // Add your logic here to handle the removal or choose not to remove
            // For example, you might want to show a message indicating that removal is not allowed
            var cart = ShoppingCart;
            cart.Remove(product);
            ShoppingCart = cart;
            // In this example, I'm just redirecting to the shopping cart view without making any changes
            Console.WriteLine($"Product with id {id} removed successfully");
            return RedirectToAction(nameof(ShoppingCartView));
        }

    }

   
}
