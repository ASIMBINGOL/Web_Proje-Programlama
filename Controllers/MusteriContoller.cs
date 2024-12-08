using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SQLitePCL;
using Web_Proje.Models;

namespace Web_Proje.Controllers
{
    public class MusteriController:Controller
    {
        private readonly KuaforContext _context;
        public MusteriController(KuaforContext context)
        {
            _context=context; //veri tabanı işlemleri için aracı 
        }

        public IActionResult Index()
        {
            return View();
        }
         public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(Musteri model)
        {
    
          _context.Musteri.Add(model); 
          await _context.SaveChangesAsync();
          return RedirectToAction("Index");

        }

        public IActionResult RandevuAl()
        {
            ViewBag.CalisanlarList=new SelectList(_context.Calisanlar.ToList(),"CalisanID","AdSoyad");
          
            return View();

        }

}
}