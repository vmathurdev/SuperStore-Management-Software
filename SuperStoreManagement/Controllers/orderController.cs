using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStoreManagement.Controllers
{
    public class orderController : Controller
    {
        private readonly SSMSContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public orderController(SSMSContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = httpContextAccessor;

        }


        public async Task<IActionResult> Index()
        {
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
            return View(await _context.order.Where(c => c.UserId == userID).ToListAsync());
        }
    }
}
