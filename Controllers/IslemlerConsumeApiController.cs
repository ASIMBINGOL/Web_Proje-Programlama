using System.Text;
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

     [HttpGet]
    public IActionResult Ekle()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Ekle(Islemler model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5269/api/IslemlerApi/", content);

            if (response.IsSuccessStatusCode)
            {
                var successMessage = await response.Content.ReadAsStringAsync();
                TempData["Success"] = successMessage;
                return RedirectToAction("Index");
            }
            else
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                TempData["Error"] = $"API Hatası: {response.StatusCode}, Detay: {errorDetails}";
                return RedirectToAction("Ekle");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Beklenmeyen bir hata oluştu: {ex.Message}";
            return RedirectToAction("Ekle");
        }
    }
}
