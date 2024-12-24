using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Proje.Models;

public class RandevuConsumeApiController : Controller
{
    private readonly KuaforContext _context;

    public RandevuConsumeApiController(KuaforContext context)
    {
        _context = context;
    }

     public async Task<IActionResult> Index()
        {
            List<Randevu> randevus = new List<Randevu>();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:5269/api/RandevuApi/");
            var jsondata = await response.Content.ReadAsStringAsync();
            randevus = JsonConvert.DeserializeObject<List<Randevu>>(jsondata);
            return View(randevus);
        }

        public ActionResult Index2(int id)
        {
            var randevular=_context.Randevular.ToList();
            return View(randevular);
        }
}
