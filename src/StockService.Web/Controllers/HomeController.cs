using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult DetailsProduct(Guid id)
        {
            var product = _context.Product.Find(id);
            return View(product);
        }

        public IActionResult EditProduct(Guid id)
        {
            var product = _context.Product.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Guid id, Product model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Product willUpdate = _context.Product.Find(model.Id);

                    willUpdate.UrunAdi = model.UrunAdi;
                    willUpdate.Kalite = model.Kalite;
                    willUpdate.Kompozisyon = model.Kompozisyon;
                    willUpdate.Miktar = model.Miktar;
                    willUpdate.Musteri = model.Musteri;
                    willUpdate.Renk = model.Renk;
                    willUpdate.RezerveDurum = model.RezerveDurum;
                    willUpdate.UrunKodu = model.UrunKodu;
                    willUpdate.ModifiedBy = User.FindFirst(ClaimTypes.Name).Value;
                    willUpdate.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    willUpdate.ModifiedDate = DateTime.UtcNow;

                    _context.SaveChanges();

                    return RedirectToAction("Product", "Home");
                }
                catch (DBConcurrencyException ex)
                {
                    if (_context.Product.Find(id) == null)
                        return NotFound();
                    throw (ex);
                }
            }
            return View(model);
        }

        public IActionResult DeleteProduct(Guid id)
        {
            var product = _context.Product.Find(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(Guid id, Product model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                Product product = _context.Product.Find(model.Id);

                _context.Product.Remove(product);
                _context.SaveChanges();

                return RedirectToAction("Product", "Home");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (_context.Product.Find(model.Id) == null)
                {
                    return NotFound();
                }
                throw (ex);
            }
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
