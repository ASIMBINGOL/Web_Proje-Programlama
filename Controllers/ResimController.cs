using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Web_Proje.Models;

public class ResimController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public ResimController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [HttpGet]
    public IActionResult Yukle()
    {
        return View();
    }

  [HttpPost]
public async Task<IActionResult> Yukle(ResimYuklemeModel model)
{
    if (model.Resim != null && model.Resim.Length > 0)
    {
        try
        {
            // Resmi byte[] olarak oku
            using var ms = new MemoryStream();
            await model.Resim.CopyToAsync(ms);
            var imageBytes = ms.ToArray();

            // RapidAPI'ye gönderim
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "d81181f3b6msh4424b43ae8fd494p1f1295jsn0057c263f8c7");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "hairstyle-changer-pro.p.rapidapi.com");

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(imageBytes), "image", model.Resim.FileName);

            // Correct task_type parametresi
            content.Add(new StringContent("hairstyle"), "task_type"); // Saç stili düzenleme işlemi için doğru parametre

            var response = await client.PostAsync("https://hairstyle-changer-pro.p.rapidapi.com/facebody/editing/hairstyle-pro", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsByteArrayAsync();
                var base64Image = Convert.ToBase64String(responseContent);
                ViewBag.Resim = base64Image;
                return View("Sonuc");
            }
            else
            {
                // Detaylı Hata Mesajını Logla
                var errorDetails = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"API Hatası: {response.StatusCode}, Detay: {errorDetails}");
                TempData["Hata"] = $"API Hatası: {response.StatusCode}, Detay: {errorDetails}";
                return RedirectToAction("Yukle");
            }
        }
        catch (Exception ex)
        {
            // Genel Hatalar
            System.Diagnostics.Debug.WriteLine($"Beklenmeyen bir hata oluştu: {ex.Message}");
            TempData["Hata"] = $"Beklenmeyen bir hata oluştu: {ex.Message}";
            return RedirectToAction("Yukle");
        }
    }

    TempData["Hata"] = "Lütfen bir resim yükleyin.";
    return RedirectToAction("Yukle");
}


}
