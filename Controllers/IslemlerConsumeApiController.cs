using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Web_Proje.Models;

public class IslemlerConsumeApiController : Controller
{
    private readonly KuaforContext _context;

    public IslemlerConsumeApiController(KuaforContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()//çalıştı
    {
      List<Islemler> Islemler = new List<Islemler>();

    using (var httpClient = new HttpClient())
    {
        var response = await httpClient.GetAsync("http://localhost:5269/api/IslemlerApi/");
        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
        try
        {
            Islemler = JsonConvert.DeserializeObject<List<Islemler>>(jsonData);
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Deserialize hatası: {ex.Message}";
        }
        }
        else
        {
            TempData["Error"] = "API'den veri alınamadı.";
        }
    }

    return View(Islemler); // Razor View'e verileri gönderiyoruz
    }

    public ActionResult Index2(int id)//çaliştı
    {
            var Islemler=_context.Islemler.ToList();
            return View(Islemler);
    }
}
