using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using Web_Proje.Models;

[Route("api/[controller]")]
[ApiController]
public class RandevuApiController : ControllerBase
{
    private readonly KuaforContext _context;

    public RandevuApiController(KuaforContext context)
    {
        _context = context;
    }

      public List<Randevu> Get()
     {
        var Randevular=_context.Randevular.ToList();

        return Randevular;
     }
}
