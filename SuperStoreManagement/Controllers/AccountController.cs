using Microsoft.AspNetCore.Mvc;
using project.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperStoreManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly SSMSContext _context;
        public AccountController(SSMSContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = httpContextAccessor;
        }


        public ActionResult Index()
        {

            return View(_context.user.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            
            if (ModelState.IsValid)
            {
                List<User> users = await _context.user.Where(u => u.UserName == user.UserName).ToListAsync();
                if (users.Count > 0)
                {
                    ViewBag.Message = "Username is not available";
                }
                else
                {
                    _context.user.Add(user);
                    _context.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Login", "Account");
                }
                //var productsWithCart = from p in User.Where(u => u.UserName == user.UserName);
               
            }
            return View();
        }
        //RedirectToAction("LoggedIn");

        public ActionResult LoginRedirect()
        {
           return RedirectToAction("Login");
        }
        public ActionResult RegisterRedirect()
        {
            return RedirectToAction("Register");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {  
            ModelState.ClearValidationState("ConfirmPassword");
            ModelState.MarkFieldValid("ConfirmPassword");
            ModelState.ClearValidationState("Email");
            ModelState.MarkFieldValid("Email");
            if (ModelState.IsValid)
            {
                var usr = _context.user.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                Debug.WriteLine("User" + usr);

                if (usr != null){
                        CookieOptions option = new CookieOptions();
                        Response.Cookies.Append("UserId", usr.UserId.ToString(), option);
                        HttpContext.Response.Cookies.Append("UserId", usr.UserId.ToString());
                        HttpContext.Response.Cookies.Append("UserName", usr.UserName.ToString());
                    
                    string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
                        return RedirectToAction("index","Home");
                    }else{
                        ModelState.AddModelError("", "Username or password is wrong.");
                    }
           
            }
            return View();
        }

       
        
        public ActionResult LoggedIn()
        {
            return View();
        }



        [HttpPost]
        public ActionResult LoggedIn(User user)
        {
            string language = HttpContext.Request.Cookies["UserId"];

            return View();
        }
    }
}
