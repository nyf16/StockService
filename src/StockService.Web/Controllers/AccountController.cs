using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockService.Domain.Identity;
using StockService.Web.ViewModels.Identity;

namespace StockService.Web.Controllers
{
    public class AccountController : Controller
    {
        // Kullanici kaydetmek icin veya kullanici bilgilerinde degisiklik yapmak icin kullanilan servis.
        private readonly UserManager<ApplicationUser> _usermanager;

        // Kullanicinin uygulamaya giris cikis islemlerini yönettigimiz servis.
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _usermanager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // gelen modeli dogrula
            if (ModelState.IsValid)
            {
                // model dogruysa
                // kullaniciyi kontrol et var mi?
                var existUser = await _usermanager.FindByEmailAsync(model.UserName);
                // yoksa hata dön
                if (existUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Bu e-mail ile kayıtlı bir kullanıcı bulunamadı!");
                    return View(model);
                }
                // kullanıcı adı ve sifre eslesiyor mu?
                var login = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                // eslesmiyorsa hat don
                if(!login.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, " Bu e-mail ve şifre ile uyumlu bir kullanıcı bulunamadı! Şifrenizi kontrol edin!");
                    return View(model);
                }

                // ana sayfaya yonlendır(simdilik)
                return RedirectToAction("Index", "Home");
            }
            // basarili degilse hata don
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            return View();
            // gelen modeli valide et
            if (ModelState.IsValid)
            {
                // validse kaydet

                // ApplicationUser olustur
                var newUser = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    // FirstName = model.FirstName,
                    // LastName = model.LastName,
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    RegisterNumber = model.RegisterNumber
                };

                var registerUser = await _usermanager.CreateAsync(newUser, model.Password);
                if (registerUser.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                // kaydetme basarisizsa hatalari modelstate ekle
                AddErrors(registerUser);
                
            
            }
            // degilse hatalari don
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }

        }

    }
}