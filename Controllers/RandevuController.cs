using Microsoft.AspNetCore.Mvc;
using Web_Proje.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Text.Json.Serialization;

public class RandevuController : Controller
{
    private readonly KuaforContext _context;

    public RandevuController(KuaforContext context)
    {
        _context = context;
    }
    

    // Randevu alma formunu göstermek için GET metodu
    [HttpGet]
    public IActionResult RandevuAl()
    {
        // Çalışanlar ve Hizmetleri view'e gönderelim
        ViewBag.Calisanlar=_context.Calisanlar.ToList();
        ViewBag.Islemler=_context.Islemler.ToList();
        
        var model = _context.CalismaSaatleri
    .Include(cs => cs.Calisan) // Çalışan bilgilerini dahil et
        .ThenInclude(c => c.islemlers) // Çalışanın işlemleri
    .Select(cs => new
    {
        cs.Calisan.CalisanID,
        cs.Calisan.AdSoyad,
        Islemler = cs.Calisan.islemlers.Select(i => i.IslemAdi).ToList(),
        Gun = cs.Gun.ToString(),
        SaatAraligi = $"{cs.SaatBaslangic:hh\\:mm} - {cs.SaatBitis:hh\\:mm}"
    })
    .ToList();
    ViewBag.CalisanlarMesai = model;

    // View'e doğru model türünü gönderin

    return View();

    }

   [HttpPost]
    public IActionResult RandevuAl(Randevu model)
    {
       if (ModelState.IsValid)
        {
         var musteriId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        // Çalışan seçilen işlemi yapabiliyor mu?


        var islemYapabiliyorMu = _context.Calisanlar
                                .Any(c => c.CalisanID == model.CalisanID && c.IslemID == model.IslemID);

        if (!islemYapabiliyorMu)
        {
            TempData["msj"] = "Seçtiğiniz çalışan bu işlemi yapamıyor. Ya İşlemi Ya Da Çalışanı Değişin";
             ViewBag.Calisanlar = _context.Calisanlar
            .Select(c => new { c.CalisanID, c.AdSoyad })//randevu al ekranına çalışan ile yapabildiği iş listelenecek.
            .ToList();

            ViewBag.Islemler = _context.Islemler
            .Select(i => new { i.IslemID, i.IslemAdi })
            .ToList();
            return RedirectToAction("RandevuAl", model); // Kullanıcıyı aynı sayfada hata mesajıyla geri döndür
        }


                    var mesaiBilgisi = _context.CalismaSaatleri
                                       .Where(c => c.CalisanID == model.CalisanID)
                                       .AsEnumerable() // Veritabanı sorgusunu burada bitirir ve linq sorgusu devam eder.
                                       .FirstOrDefault(c => model.Saat >= c.SaatBaslangic 
                                        && model.Saat <= c.SaatBitis);


             if (mesaiBilgisi == null)
             {
                  // Eğer mesai saatleri içinde değilse, hata mesajı glönder
                TempData["msj"]="Bu Saat de Çalışanımız Çalışmıyor Ya Çalışanı Ya Da Saati Değişin";
                return RedirectToAction("RandevuAl",model);
            }


            var MesaiGunu =_context.CalismaSaatleri.Where(c=>c.CalisanID==model.CalisanID).Select(c=>c.Gun==model.Tarih.DayOfWeek);


             if (MesaiGunu == null)
             {
                  // Eğer mesai saatleri içinde değilse, hata mesajı glönder
                TempData["msj"]="Bu Gunde de Çalışanımız Çalışmıyor Ya Çalışanı Ya Da Gunu Değişin";
                return RedirectToAction("RandevuAl",model);
            }




            // Aynı çalışana, aynı tarih ve saatte başka bir randevu varsa hata döndür
            var mevcutRandevu = _context.Randevular
                                .FirstOrDefault(r =>
                                r.CalisanID == model.CalisanID &&
                                r.Tarih.Date == model.Tarih.Date &&
                                r.Saat == model.Saat);
            
            if (mevcutRandevu != null)
            {
                // Çalışanlar ve işlemler listesini tekrar doldur
                 ViewBag.Calisanlar = _context.Calisanlar
                                     .Select(c => new { c.CalisanID, c.AdSoyad })
                                     .ToList();

                ViewBag.Islemler = _context.Islemler
                                   .Select(i => new { i.IslemID, i.IslemAdi })
                                   .ToList();
               TempData["msj"]="Bu Saat de Çalışanımız Dolu Lütfen Çalışanı Ya Da Saati Değişin";
                return RedirectToAction("RandevuAl",model);
            }




             model.MusteriID = int.Parse(musteriId);

            // Randevuyu kaydet
            _context.Randevular.Add(model);
            _context.SaveChanges();

            // Başarılı bir işlem sonrası listeye yönlendirme
            return RedirectToAction("Index","Musteri"); //Randevularima yönlendirilecek.
        }

        // Model hatalıysa tekrar aynı View döndürülür
        ViewBag.Calisanlar = _context.Calisanlar
                            .Select(c => new { c.CalisanID, c.AdSoyad })
                            .ToList();
        ViewBag.Islemler = _context.Islemler
                            .Select(i => new { i.IslemID, i.IslemAdi })
                            .ToList();
        return View(model);
    }


   /*  public IActionResult RandevuListesi()
    {
        var randevular = _context.Randevular
            .Include(r => r.CalisanID)
            .Include(r => r.IslemID)
            .ToList();
        return View(randevular);
    }*/


}

    // Randevuyu onaylamak için GET metodu
  /*  [HttpGet]
    public async Task<IActionResult> RandevuOnay(int randevuId)
    {
        var randevu = await _context.Randevular.FindAsync(randevuId);
        if (randevu == null)
            return NotFound(new { message = "Randevu bulunamadı." });

        // Randevu bilgilerini view'e gönder
        return View(randevu);
    }*/
