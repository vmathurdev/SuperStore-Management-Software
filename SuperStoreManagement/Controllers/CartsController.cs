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
    public class CartsController : Controller
    {
        private readonly SSMSContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartsController(SSMSContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = httpContextAccessor;

        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.product.ToListAsync();
            List<Cart> cart = await _context.Cart.ToListAsync();
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            int userID;

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
                                   on p.prodID equals c.ProductID  into table1
                                   from c in table1.ToList()
                                   where c.UserID == userID
                                   select new Home { Products = p, Carts = c, };
            return View(productsWithCart);
        }


        [HttpPost]
        public ActionResult Checkout(IEnumerable<Home> model)
        {
            
            return RedirectToAction("pay", "Product");
        }

        [HttpPost]
        public async Task<ActionResult> Payment()
        {
            List<Product> products = await _context.product.ToListAsync();
            List<Cart> cart = await _context.Cart.ToListAsync();
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            int userID;
            float total = 0;
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
            foreach(var item in productsWithCart)
            {
                _context.Cart.Remove(item.Carts);
                total = item.Products.Price * item.Carts.Quanity;
            }

            return RedirectToAction("Index", "Home");

        }
        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.CartID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartID,Quanity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartID,Quanity")] Cart cart)
        {
            if (id != cart.CartID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartID))
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
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.CartID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.CartID == id);
        }
    }
}
