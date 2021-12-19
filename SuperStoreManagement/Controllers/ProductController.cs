using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperStoreManagement.Models;
using project.Models;
using Microsoft.AspNetCore.Http;

namespace SuperStoreManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly SSMSContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(SSMSContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = httpContextAccessor;

        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.product.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.product
                .FirstOrDefaultAsync(m => m.prodID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("prodID,Name,Description,Price,image,stock,category")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("prodID,Name,Description,Price,image,stock,category")] Product product)
        {
            if (id != product.prodID)
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
                    if (!ProductExists(product.prodID))
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
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.product
                .FirstOrDefaultAsync(m => m.prodID == id);
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
            var product = await _context.product.FindAsync(id);
            _context.product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.product.Any(e => e.prodID == id);
        }
        [Route("Pay")]
        public IActionResult Pay()
        {
            return View("Payment");
        }

        [HttpPost]
        public async Task<ActionResult> paid()
        {
            List<Product> products = await _context.product.ToListAsync();
            List<Cart> cart = await _context.Cart.ToListAsync();
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            int userID;
            float total = 0;
            int totalItem = 0;
            if (String.IsNullOrEmpty(cookieValueFromContext))
            {
                userID = 0;
            }
            else
            {
                userID = int.Parse(cookieValueFromContext);
            }
            var productsWithCart = from p in products
                                   join c in cart.Where(c => c.UserID == userID)
                                   on p.prodID equals c.ProductID into table1
                                   from c in table1.ToList()
                                   where c.UserID == userID
                                   select new Home { Products = p, Carts = c, };
            foreach (var item in productsWithCart)
            {
                int id = item.Carts.CartID;
                _context.Cart.Remove(item.Carts);
                await _context.SaveChangesAsync();
                total = item.Products.Price * item.Carts.Quanity;
                totalItem = totalItem + 1;
            }

            Order order = new Order();
            order.totalPrice = total;
            order.date = DateTime.Now;
            order.UserId = userID;
            _context.order.Add(order);
            await _context.SaveChangesAsync();
            // custom implementation
            return RedirectToAction("Index", "order");
        }
    }
}
