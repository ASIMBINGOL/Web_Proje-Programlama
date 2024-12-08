using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Web_Proje.Models;

namespace Web_Proje.Controllers;

public class HomeController : Controller
{
       private readonly KuaforContext _context;
        public HomeController(KuaforContext context)
        {
            _context=context; //veri tabanı işlemleri için aracı 
        }

    public IActionResult Index()
    {
        return View();
    }

   /*  public async Task<IActionResult> CalisanBilgi()
    {
        var CalisanUzmBilgi= await _context.Calisanlar.Include(x=>x.Aciklama).Include(x=>x.islemler).ThenInclude(x=>x.IslemAdi).ToListAsync();
           return View(CalisanUzmBilgi);
    }*/

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
