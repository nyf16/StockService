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
        private readonly UserManager<ApplicationUser> _userManager;

        // Kullanicinin uygulamaya giris cikis islemlerini yönettigimiz servis.
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl = null)
        {
            // gelen modeli dogrula
            if (ModelState.IsValid)
            {
                // model dogruysa
                // kullaciyi kontrol et var mi ?
                var existUser = await _userManager.FindByEmailAsync(model.Username);
                // yoksa hata don
                if (existUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Bu e-mail ile kayıtlı bir kullanıcı bulunamadı!");
                    return View(model);
                }
                // kullanici adi ve sifre eslesiyor mu ?
                var login = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                // eslesmiyorsa hata don
                if (!login.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Bu e-mail ve şifre ile uyumlu bir kullanıcı bulunamadı! Şifrenizi kontrol edin!");
                    return View(model);
                }

                //ana sayfaya yönlendir simdilik
                if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

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
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    RegisterNumber = model.RegisterNumber
                };

                var registerUser = await _userManager.CreateAsync(newUser, model.Password);
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}