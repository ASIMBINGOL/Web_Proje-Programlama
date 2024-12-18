using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Proje.Models;

namespace Web_Proje.Controllers
{
    public class RandevuController:Controller
    {
        private readonly KuaforContext _context;
        public RandevuController(KuaforContext context)
        {
            _context=context; //veri tabanı işlemleri için aracı 
        }



        [HttpGet]
[HttpGet]
public async Task<IActionResult> GetHizmetler(int calisanId)
{
    var hizmetler = await _context.Calisanlar
        .Where(c => c.CalisanID == calisanId)
        .SelectMany(c => c.islemlers) // Çalışanın yaptığı işlemleri alıyoruz
        .Select(i => new
        {
            i.IslemID,
            i.IslemAdi
        })
        .ToListAsync();

    return Json(hizmetler);
}



[HttpGet]
public async Task<IActionResult> GetUygunSaatler(int calisanId, DateTime tarih, int randevuSuresi)
{
    var calismaSaatleri = await _context.CalismaSaatleri
        .Where(cs => cs.CalisanID == calisanId && cs.Gun == tarih.Day)
        .Select(cs => new
        {
            cs.BaslangicSaati,
            cs.BitisSaati
        })
        .FirstOrDefaultAsync();

    if (calismaSaatleri == null)
        return Json(new List<string>()); // Çalışma saati yoksa boş liste döner

    var mevcutRandevular = await _context.Randevular
        .Where(r => r.CalisanID == calisanId && r.Tarih.Date == tarih.Date)
        .Select(r => r.Saat)
        .ToListAsync();

    var uygunSaatler = new List<string>();
    var baslangic = calismaSaatleri.BaslangicSaati;
    var bitis = calismaSaatleri.BitisSaati;

    while (baslangic.AddMinutes(randevuSuresi) <= bitis)
    {
        if (!mevcutRandevular.Contains(baslangic))
            uygunSaatler.Add(baslangic.ToString(@"hh\:mm"));

        baslangic = baslangic.AddMinutes(randevuSuresi);
    }

    return Json(uygunSaatler);
}


        [HttpGet]
    public async Task<IActionResult> RandevuAl([FromForm]int id)
    {
        // Çalışanları ve hizmetleri listele
        var calisanlar = await _context.Calisanlar.ToListAsync();
       var islemler = await _context.Randevular
            .Where(x => x.IslemID == id) // Filtreleme
            .Select(x => x.IslemID) // Sadece 'Islem' property'i alınır
            .ToListAsync();

        ViewBag.Calisanlar = calisanlar;
        ViewBag.Islemler = islemler;

        return View();
    }
    

   [HttpPost]
public async Task<IActionResult> RandevuAl(Randevu model)
{
    if (ModelState.IsValid)
    {
        // Kullanıcı oturum bilgisinden müşteri ID'sini al
        var musteriId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (musteriId == null)
            return RedirectToAction("Login", "LogIn");

        // Randevu oluştur
        var yeniRandevu = new Randevu
        {
            MusteriID = int.Parse(musteriId),
            CalisanID = model.CalisanID,
            IslemID = model.IslemID,
            Tarih = model.Tarih,
            Saat=model.Saat
        };

        _context.Randevular.Add(yeniRandevu);
        await _context.SaveChangesAsync();

        return RedirectToAction("Randevularim", "Musteri");
    }

    // Çalışan ve hizmet listelerini yeniden yükle
    ViewBag.Calisanlar = await _context.Calisanlar.ToListAsync();
    ViewBag.Islemler = await _context.Islemler.ToListAsync();

    return View(model);
}
    }
}