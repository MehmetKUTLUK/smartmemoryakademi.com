using Education.Areas.Admin.Models;
using Education.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Text;

namespace Education.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Administrators")]
public class PromoCodesController : Controller
{
   
    private readonly string entityName = "Kodu üretme";

    private readonly AppDbContext context;
    private readonly IConfiguration configuration;


    public PromoCodesController(
        AppDbContext context,
        IConfiguration configuration

        )
    {
        this.context = context;
        this.configuration = configuration;

    }
    private static string GeneratePromoCode(int length)
    {
        const string chars = "0123456789";//1 haneli sayıdan oluşacak tekrarsız !
        var random = new Random();
        var code = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            code.Append(chars[random.Next(chars.Length)]);
        }

        return code.ToString();
    }

    public async Task<IActionResult> TableData(Guid? id, DataTableParameters parameters)
    {
        var query = context.PromoCodes;

        var result = new DataTableResult
        {
            data = await query
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Code,
                    p.Enabled,        
                   
                    p.CodeUserName

                }).ToListAsync(),
            draw = parameters.Draw,
            recordsFiltered = await query.CountAsync(),
            recordsTotal = await query.CountAsync()
        };

        return Json(result);
    }
    
    public async Task< IActionResult> Index()
    {

     

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(PromoCode model)
    {

        string promoCode = GeneratePromoCode(11); // 8 karakter uzunluğunda bir promosyon kodu üretir

        var promoCodeEntity = new PromoCode
        {
            Code = promoCode,

            CreatedAt = DateTime.UtcNow
        };
        string kod = promoCodeEntity.Code;
        context.PromoCodes.Add(promoCodeEntity);
        context.SaveChanges();

        return View(model);
    }


}




