using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        return View(); // Razor View'i çağırır
    }

[HttpPost]
public async Task<IActionResult> Yukle(IFormFile image)
{
    if (image == null || image.Length == 0)
    {
        ModelState.AddModelError("", "Lütfen bir resim yükleyin.");
        return View("Index"); 
    }

    using var httpClient = new HttpClient();
    using var formData = new MultipartFormDataContent();

    // Resmi ekle
    using var stream = new MemoryStream();
    await image.CopyToAsync(stream);
    var content = new ByteArrayContent(stream.ToArray());
    content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
    formData.Add(content, "image", image.FileName);

    // API isteği gönder
    var response = await httpClient.PostAsync("https://rapidapi.com", formData);

    if (response.IsSuccessStatusCode)
    {
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return View("Sonuc", jsonResponse); 
    }
    else
    {
        ModelState.AddModelError("", "API isteği başarısız oldu.");
        return View("Index"); 
    }
}

}

