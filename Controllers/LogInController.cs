
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Proje.Models;

namespace Web_Proje.Controllers
{
    public class LogInController:Controller
    {
        private readonly KuaforContext _context;
        public LogInController(KuaforContext context)
        {
            _context=context; //veri tabanı işlemleri için aracı 
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel model)
        {
            if(ModelState.IsValid)
            {
                var kullanici = await _context.Musteri.FirstOrDefaultAsync(x => x.MUsteriEmail == model.Email && x.MUsteriSifre == model.Sifre);

                var admin = await _context.Admin.FirstOrDefaultAsync(x =>x.AdminEmail == model.Email && x.AdminSifre == model.Sifre);

                if(kullanici != null)
                {
                    var kullaniciBilgileri = new List<Claim>();

                    kullaniciBilgileri.Add(new Claim(ClaimTypes.NameIdentifier, kullanici.MUsteriID.ToString()));
                    kullaniciBilgileri.Add(new Claim(ClaimTypes.GivenName, kullanici.MUsteriAd ?? ""));

                    kullaniciBilgileri.Add(new Claim(ClaimTypes.Role, "user"));


                    var claimsIdentity = new ClaimsIdentity(kullaniciBilgileri, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties 
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), 
                        authProperties);

                    return RedirectToAction("Index","Musteri");
                }
                else if(admin != null)
                {
                    var adminBilgileri = new List<Claim>();

                    adminBilgileri.Add(new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()));
                    adminBilgileri.Add(new Claim(ClaimTypes.Name, ""));

                    adminBilgileri.Add(new Claim(ClaimTypes.Role, "admin"));

                    var claimsIdentity = new ClaimsIdentity(adminBilgileri, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties 
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), 
                        authProperties);

                    return RedirectToAction("Index","Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                }
            } 
            
            return View(model);
        }
    }
}        