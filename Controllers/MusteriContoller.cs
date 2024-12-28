using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        return RedirectToAction("Login","LogIn");
        }
        return View(model);
        }  
        [Authorize(Roles ="user")]
        public IActionResult Randevularim()
        {
            // Oturumdan müşteri ID'sini al
            var musteriId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (musteriId == null)
            {
                // Eğer oturumda müşteri ID'si yoksa, giriş sayfasına yönlendirin
                TempData["msj"]="İlk Önce Giriş Yapın";
                return RedirectToAction("Login", "LogIn");
            }

           int musteriIdint=int.Parse(musteriId);

            // LINQ sorgusu
            var randevular = (from r in _context.Randevular
                            join c in _context.Calisanlar on r.CalisanID equals c.CalisanID
                            join i in _context.Islemler on r.IslemID equals i.IslemID
                            join m in _context.Musteri on r.MusteriID equals m.MUsteriID
                            where r.MusteriID == musteriIdint // Sadece oturumdaki müşteri ID'sini al
                            select new RandevuViewModel
                            {
                                RandevuID = r.RandevuID,
                                MusteriAdSoyad = m.AdSoyad,
                                CalisanAdSoyad = c.AdSoyad,
                                IslemAdi = i.IslemAdi,
                                Tarih = r.Tarih,
                                Saat = r.Saat,
                                OnayDurumu2=r.OnayDurumu
                                
                            }).ToList();

            return View(randevular);
        }
   
    [HttpGet]
        public async Task<IActionResult>Edit(int? id)
        {
            if(id==null){return NotFound();}
             var MusteriEdit =await _context.Musteri.FirstOrDefaultAsync(o=>o.MUsteriID==id);
            if(MusteriEdit==null){return NotFound();}
            return View(MusteriEdit);
        }

        
        [HttpPost]
        public async Task<IActionResult>Edit(int id,Musteri model)
        {
          if(id!=model.MUsteriID)
          {
            return NotFound();
          }
          if(ModelState.IsValid)
          {
            try
            {   
                _context.Update(model);//değişiklikleri yap
                await _context.SaveChangesAsync();//veri tabanına uygula

            }
            catch(DbUpdateConcurrencyException)
            {
                if(!_context.Musteri.Any(o=>o.MUsteriID==model.MUsteriID)){
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
           return RedirectToAction("Index","Musteri");
          }
          return View(model);
        }
    }
}