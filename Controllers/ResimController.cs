using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Proje.Models;
[Authorize(Roles ="user")]
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
public async Task<IActionResult> Yukle(IFormFile image, string hairType)
{
    if (image == null || image.Length == 0)
    {
        ModelState.AddModelError("", "Lütfen bir resim yükleyin.");
        return View("Yukle");
    }

    if (string.IsNullOrEmpty(hairType))
    {
        ModelState.AddModelError("", "Lütfen bir saç tipi belirtin.");
        return View("Yukle");
    }

    var apiKey = "af661ef0b7msh0910117e83095d0p16fca2jsnec5fe1db4c4c";
    var apiHost = "hairstyle-changer.p.rapidapi.com";
    var apiUrl = "https://hairstyle-changer.p.rapidapi.com/huoshan/facebody/hairstyle";

    using var httpClient = new HttpClient();
    using var formData = new MultipartFormDataContent();

    using (var stream = new MemoryStream())
    {
        await image.CopyToAsync(stream);
        var imageContent = new ByteArrayContent(stream.ToArray());
        imageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "image_target",
            FileName = image.FileName
        };
        formData.Add(imageContent, "image_target");
    }

    var hairTypeContent = new StringContent(hairType);
    hairTypeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
    {
        Name = "hair_type"
    };
    formData.Add(hairTypeContent);

    httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);
    httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", apiHost);

    var response = await httpClient.PostAsync(apiUrl, formData);

    if (response.IsSuccessStatusCode)
    {
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // JSON verisini ayrıştır
        var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonResponse);
        string base64Image = responseObject?.data?.image;

        if (!string.IsNullOrEmpty(base64Image))
        {
            ViewBag.Base64Image = base64Image; // Base64 veriyi ViewBag ile View'a gönderiyoruz.
            return View("Sonuc");
        }
        else
        {
            ModelState.AddModelError("", "API yanıtında görsel verisi bulunamadı.");
            return View("Yukle");
        }
    }
    else
    {
        var errorDetails = await response.Content.ReadAsStringAsync();
        ModelState.AddModelError("", $"API isteği başarısız oldu: {errorDetails}");
        return View("Yukle");
    }
}

[AllowAnonymous]
public IActionResult Error(string? url){
    return View();

}


}

