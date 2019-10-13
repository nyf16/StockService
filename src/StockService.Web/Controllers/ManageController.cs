using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StockService.Web.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        public IActionResult EditRole()
        {
            return View();
        }
        public IActionResult DeletRole()
        {
            return View();
        }
        public IActionResult AssingRole(string userId, string roleId)
        {
            return View();
        }
        public IActionResult RevekoRole(string userId, string roleId)
        {
            return View();
        }


    }   
        
}