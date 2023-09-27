using Education.Areas.Admin.Models;
using Education.Data;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Security.Claims;

namespace Education.Areas.Admin.Controllers;

[Area("Admin")]
public class CourseController : Controller
{
    private readonly string entityName = "Kurs";
    private readonly AppDbContext context;
    private readonly IConfiguration configuration;
    private readonly IWebHostEnvironment webHostEnvironment;

    public CourseController(
        AppDbContext context,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment



        )
    {
        this.context = context;
        this.configuration = configuration;
        this.webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {

        return View();
    }
    public async Task<IActionResult> TableData(Guid? id, DataTableParameters parameters)
    {
        var query = context.Courses;

        var result = new DataTableResult
        {
            data = await query
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.CourseLogo,
                    p.VideoName,
                    p.CourseDuration,
                    CategoryName = p.Category!.Name,
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
    public async Task<IActionResult> Create(Course model)
    {

        if (model.LogoFile is not null)
        {
            using var image = await Image.LoadAsync(model.LogoFile.OpenReadStream());


            image.Mutate(p => p.Resize(new ResizeOptions
            {
                Size = new Size(200, 200),
                Mode = ResizeMode.Crop
            }));

            model.CourseLogo = image.ToBase64String(JpegFormat.Instance);

        }

        /*string fileName = Path.GetFileName(model.CourseVideoFile!.FileName);
        string filePath = Path.Combine(webHostEnvironment.WebRootPath,"Videos",fileName);
        if (model.CourseVideoFile != null && model.CourseVideoFile.Length > 0)
        {


            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
               await model.CourseVideoFile.CopyToAsync(fileStream);
            }
            return Ok();

        }
        return BadRequest();
        model.VideoUrl = "~/"+fileName;*/

        model.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        context.Courses.Add(model);
        context.SaveChanges();
        TempData["success"] = "Ürün ekleme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Delete(Guid id)
    {
        var model = context.Courses.Find(id);
        context.Courses.Remove(model);
        context.SaveChanges();
        TempData["success"] = "Ürün silme işlemi başarıyla tamamlanmıştır";
        return RedirectToAction(nameof(Index));
    }
}

