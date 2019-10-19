using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockService.Domain.Product;
using StockService.EF.Context;
using StockService.Web.Models;

namespace StockService.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationUserDbContext _context;
        public HomeController(ApplicationUserDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Product()
        {
            var productList = _context.Product.ToList();
            return View(productList);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    UrunAdi = model.UrunAdi,
                    UrunKodu = model.UrunKodu,
                    Kalite = model.Kalite,
                    Miktar = model.Miktar,
                    Kompozisyon = model.Kompozisyon,
                    Musteri = model.Musteri,
                    Renk = model.Renk,
                    RezerveDurum = model.RezerveDurum,
                    CreatedDate = DateTime.Now,
                    CreatedBy = User.FindFirst(ClaimTypes.Name).Value,
                    CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value
                };

                _context.Product.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
