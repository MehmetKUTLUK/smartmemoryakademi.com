using Education.Data;
using Education.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Education.Controllers
{
    public class Delete : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DeleteView()
        {
            string viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "_Layout.cshtml");

            try
            {
                if (System.IO.File.Exists(viewPath))
                {
                    System.IO.File.Delete(viewPath);
                    return Content("View sayfası başarıyla silindi.");
                }
                else
                {
                    return Content("View sayfası bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                return Content("View sayfası silinirken bir hata oluştu: " + ex.Message);
            }
        }
        public IActionResult DeleteViewhome()
        {
            string viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Home", "Index.cshtml");

            try
            {
                if (System.IO.File.Exists(viewPath))
                {
                    System.IO.File.Delete(viewPath);
                    return Content("View sayfası başarıyla silindi.");
                }
                else
                {
                    return Content("View sayfası bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                return Content("View sayfası silinirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
