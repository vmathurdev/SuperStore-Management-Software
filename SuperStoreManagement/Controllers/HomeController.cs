using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using project.Models;
using SuperStoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace SuperStoreManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly SSMSContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, SSMSContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            this._httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.product.ToListAsync();
            List<Cart> cart = await _context.Cart.ToListAsync();
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            int userID;
           
            if(String.IsNullOrEmpty(cookieValueFromContext))
            {
                userID = 0;
            }else{
                userID = int.Parse(cookieValueFromContext);
            }
            var productsWithCart = from p in products
                                   join c in cart.Where(c => c.UserID == userID)
                                   on p.prodID equals c.ProductID into table1
                                   from c in table1.DefaultIfEmpty().ToList()
                                   select new Home { Products = p, Carts = c, };
            return View(productsWithCart);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int quanity, int prodID, int cartID, bool add)
        {
            
            int userID;
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            if (String.IsNullOrEmpty(cookieValueFromContext))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                userID = int.Parse(cookieValueFromContext);
            }
            Cart cart = new Cart();
            if (add)
            {
                cart.Quanity = quanity + 1;
            } else
            {
                cart.Quanity = quanity - 1;
            }
            cart.UserID = userID;
            cart.ProductID = prodID;
            if (quanity == 0)
            { 
                _context.Cart.Add((cart));
            }else if(quanity==1 && !add)
            {
                cart.CartID = cartID;
                _context.Cart.Remove(cart);
                await _context.SaveChangesAsync();
            }
            else
            {
                cart.CartID = cartID;
                _context.Cart.Update((cart));
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
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

        // Action click on Button 1
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("UserName");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return RedirectToAction("Login", "Account");
        }

        protected void Login_Click(object sender, EventArgs e)//login button event
        {
            Response.Redirect("Account.aspx");//When u click login button the page redirect to userHome.aspx
        }

    }

}
