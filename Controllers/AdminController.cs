using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Proje.Models;

namespace Web_Proje.Controllers
{
    public class AdminController:Controller
    {
       private readonly KuaforContext _context;
        public AdminController(KuaforContext context)
        {
            _context=context; //veri tabanı işlemleri için aracı 
        }

       public IActionResult Index()
       {
            return View();
       } 

        public async Task<IActionResult>CalisanEKle()
        {
            ViewBag.IslemlerListe= new SelectList(await _context.Islemler.ToListAsync(),"IslemID","IslemAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>CalisanEkle(Calisan model)
        {
            _context.Calisanlar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Admin");
        }

        public IActionResult IslemEKle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>IslemEKle(Islemler model)//async çünkü bir sipariş alındığı zaman arka da hazırlanır ama sipariş alınmaya devam eder
        {
            _context.Islemler.Add(model);
            await _context.SaveChangesAsync(); //async olduğu için await önemli
            return RedirectToAction("Index","Admin");

        }
    }
}   