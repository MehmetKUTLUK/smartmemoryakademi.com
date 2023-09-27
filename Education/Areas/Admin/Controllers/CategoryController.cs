using Education.Areas.Admin.Models;
using Education.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace Education.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrators")]
public class CategoryController : Controller
{
    private readonly string entityName = "Kategori";

    private readonly AppDbContext context;
    private readonly IConfiguration configuration;


    public CategoryController(
        AppDbContext context,
        IConfiguration configuration

        )
    {
        this.context = context;
        this.configuration = configuration;

    }
    public IActionResult Index()
    {
        var model = context.Categories.OrderBy( c => c.Name ).ToList(); 
        return View();
    }
    public async Task<IActionResult> TableData(Guid? id, DataTableParameters parameters)
    {
        var query = context.Categories;

        var result = new DataTableResult
        {
            data = await query
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Logo,
                    UserName = p.CreatorUser!.Name,
                    dateCreated = p.DateCreated.ToLocalTime().ToShortDateString(),
                }).ToListAsync(),
            draw = parameters.Draw,
            recordsFiltered = await query.CountAsync(),
            recordsTotal = await query.CountAsync()
        };

        return Json(result);
    }
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = new SelectList(await context.Categories.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");

        return View();
    }
    [HttpPost]

    public async Task<IActionResult> Create(Category model)
    {
        ViewBag.Categories = new SelectList(await context.Categories.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");


        if (model.LogoFile is not null)
        {
            using var image = await Image.LoadAsync(model.LogoFile.OpenReadStream());


            image.Mutate(p => p.Resize(new ResizeOptions
            {
                Size = new Size(200, 200),
                Mode = ResizeMode.Crop
            }));

            model.Logo = image.ToBase64String(JpegFormat.Instance);

        }
       



        try
        {

            model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            context.Categories.Add(model);
            context.SaveChanges();
            TempData["success"] = "Ürün ekleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));

        }
        catch (DbUpdateException)
        {
            TempData["error"] = "Şirket Logosu Ve Bayrak Alanı boş Bırakılamaz";
            return View(model);
        }



    }
    public async Task<IActionResult> Edit(Guid id)
    {
        ViewBag.Categories = new SelectList(await context.Categories.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");

        var model = context.Categories.Find(id);
        
        return View(model);
    }
    [Authorize(Roles = "Administrators")]
    [HttpPost]
    public async Task<IActionResult> Edit(Category model)
    {



        if (model.LogoFile is not null)
        {
            using var image = await Image.LoadAsync(model.LogoFile.OpenReadStream());


            image.Mutate(p => p.Resize(new ResizeOptions
            {
                Size = new Size(250, 250),
                Mode = ResizeMode.Crop
            }));

            model.Logo = image.ToBase64String(JpegFormat.Instance);

        }
        

        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        context.Categories.Add(model);

        context.Categories.Update(model);
        context.SaveChanges();
        TempData["success"] = "Ürün güncelleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));





    }
    [Authorize(Roles = "Administrators")]
    public IActionResult Delete(Guid id)
    {
        var model = context.Categories.Find(id);
        context.Categories.Remove(model);
        context.SaveChanges();
        TempData["success"] = "Ürün silme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
}
