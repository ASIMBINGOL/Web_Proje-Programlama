using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Proje.Models;

namespace Web_Proje.Controllers
{
    [Authorize(Roles ="admin")]
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

       public async Task<IActionResult> IslemListele()
       {
            return View(await _context.Islemler.ToListAsync());
       }

    
    public async Task <IActionResult> CalisanListele()
      { 
        var clsnListe=(from c in _context.Calisanlar
                      join i in _context.Islemler
                      on c.IslemID equals i.IslemID
                      select new CalisanIslemModel
                      {
                              CalisanID = c.CalisanID,
                              CalisanAd = c.CalisanAd,
                              CalisanSoyad = c.CalisanSoyad,
                              Aciklama=c.Aciklama,               //İŞlemi çalışanda gösterme
                              CalisanTelefon = c.CalisanTelefon,
                              CalisanEmail = c.CalisanEmail,
                              IslemAdi = i.IslemAdi  // İşlem adını alıyoruz
                          }).ToListAsync();
                return View(await clsnListe); 
      }

        

       [HttpGet]
        public async Task<IActionResult>CalisanEdit(int? id)
        {

            ViewBag.IslemlerListe= new SelectList(await _context.Islemler.ToListAsync(),"IslemID","IslemAdi");
            if(id==null){return NotFound();}
             var calisanedit =await _context.Calisanlar.FirstOrDefaultAsync(o=>o.CalisanID==id);
              // var ogr =await _context.Ogrenciler.FirstOrDefaultAsync(ogrenci =>ogrenci.OgrenciId==id);//başka kriterlere göre arama ilk gelen eşitliği geri dönderir
            if(calisanedit==null){return NotFound();}
            return View(calisanedit);
        }

        
        [HttpPost]
        public async Task<IActionResult>CalisanEdit(int id,Calisan model)
        {
          if(id!=model.CalisanID)
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
                if(!_context.Calisanlar.Any(o=>o.CalisanID==model.CalisanID)){
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
           return RedirectToAction("CalisanListele");
          }
          return View(model);
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
            //ViewBag.Calisanlae=new SelectList()
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>IslemEKle(Islemler model)//async çünkü bir sipariş alındığı zaman arka da hazırlanır ama sipariş alınmaya devam eder
        {
            _context.Islemler.Add(model);
            await _context.SaveChangesAsync(); //async olduğu için await önemli
            return RedirectToAction("Index","Admin");
                    //deneme repo
        }



        [HttpGet]
        public async Task<IActionResult>IslemEdit(int? id)
        {
            ViewBag.IslemlerListe= new SelectList(await _context.Islemler.ToListAsync(),"IslemAdi","IslemAdi");
            if(id==null){return NotFound();}
             var IslemEdit =await _context.Islemler.FirstOrDefaultAsync(o=>o.IslemID==id);
              // var ogr =await _context.Ogrenciler.FirstOrDefaultAsync(ogrenci =>ogrenci.OgrenciId==id);//başka kriterlere göre arama ilk gelen eşitliği geri dönderir
            if(IslemEdit==null){return NotFound();}
            return View(IslemEdit);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>IslemEdit(int id,Islemler model)
        {
          if(id!=model.IslemID)
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
                if(!_context.Islemler.Any(o=>o.IslemID==model.IslemID)){
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
           return RedirectToAction("IslemListele","Admin");
          }
          return View(model);
        }
    
       [HttpGet]
       public async Task<IActionResult>IslemDelete(int? id)
       {
        if(id==null){
            return NotFound();
        }
        var IslemDelete =await _context.Islemler.FindAsync(id);
        if(IslemDelete==null) {return NotFound();}
        return View(IslemDelete);
       }

       [HttpPost]
       
       public async Task<IActionResult>IslemDelete([FromForm] int id)
       {
        var IslemDelete=await _context.Islemler.FindAsync(id);
        if(IslemDelete==null){return NotFound();}
        _context.Islemler.Remove(IslemDelete);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
       }



    public IActionResult Onayla()
    {
           var randevular = (from r in _context.Randevular
                      join c in _context.Calisanlar on r.CalisanID equals c.CalisanID
                      join i in _context.Islemler on r.IslemID equals i.IslemID
                      join m in _context.Musteri on r.MusteriID equals m.MUsteriID
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
    public async Task<ActionResult> Onayla(int id)
    {
        // Randevular tablosundan id'ye göre ilgili randevuyu al
        var randevu = await _context.Randevular.FirstOrDefaultAsync(r => r.RandevuID == id);

        if (randevu != null)
        {
            // OnayDurumu'nu "Onayla" olarak değiştir
            randevu.OnayDurumu = true;

            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();
        }

          var randevular = (from r in _context.Randevular
                      join c in _context.Calisanlar on r.CalisanID equals c.CalisanID
                      join i in _context.Islemler on r.IslemID equals i.IslemID
                      join m in _context.Musteri on r.MusteriID equals m.MUsteriID
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

    public IActionResult CalisanKazancRaporu()
    {
        var kazancRaporu = _context.Randevular
            .Where(r => r.OnayDurumu) // Sadece onaylanmış randevular
            .GroupBy(r => r.CalisanID) // ÇalışanID'ye göre gruplama
            .Select(grup => new CalisanKazancViewModel
            {
                CalisanID = grup.Key,
                CalisanAdSoyad = _context.Calisanlar.FirstOrDefault(c => c.CalisanID == grup.Key).AdSoyad,
                ToplamKazanc = grup.Sum(r => _context.Islemler.FirstOrDefault(i => i.IslemID == r.IslemID).Ucret)
            })
            .ToList();

        return View(kazancRaporu);
    }


    }
}   