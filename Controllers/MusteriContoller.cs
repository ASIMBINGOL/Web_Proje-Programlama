using System.Security.Claims;
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
        if (ModelState.IsValid)
        {
        // Veritabanına kaydetme işlemi
        model.KayitTarihi=DateTime.Now;
        _context.Musteri.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
        }
        return View(model);
        }  
  

    }
}