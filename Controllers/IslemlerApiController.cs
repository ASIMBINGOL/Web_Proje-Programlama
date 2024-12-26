using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using Web_Proje.Models;

[Route("api/[controller]")]
[ApiController]
public class IslemlerApiController : ControllerBase
{
    private readonly KuaforContext _context;

    public IslemlerApiController(KuaforContext context)
    {
        _context = context;
    }
     [HttpGet]
      public List<Islemler> Get()
     {
        var Islemler=_context.Islemler.ToList();

        return Islemler;
     }

    [HttpGet("{id}")]
    public ActionResult<Islemler> Get(int id)
    {
        var Islemler1 =_context.Islemler.FirstOrDefault(x=>x.IslemID==id);
        if (Islemler1 is null)
        {
            return NoContent();
        }
            return Islemler1;
    }

    [HttpPost]
    public ActionResult Post([FromBody] Islemler y)
    {
        _context.Islemler.Add(y);
        _context.SaveChanges();
        return Ok(y.IslemAdi+" işlemi eklendi");

    }
}
