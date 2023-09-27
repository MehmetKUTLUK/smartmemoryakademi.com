using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Education.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Members")]
    public class StudentDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Video()
        {
            return View();
        }
        public IActionResult SleepEdu()
        {
            return View();
        }
        public IActionResult Meditations()
        {
            return View();
        }
        public IActionResult Read()
        {
            return View();
        }
        public IActionResult Translate()
        {
            return View();
        }
        public IActionResult Earth()
        {
            return View();
        }
        public IActionResult Begginer()
        {
            return View();
        }
        public IActionResult Elementary()
        {
            return View();
        }
        public IActionResult Intermediate()
        {
            return View();
        }
        public IActionResult Uppermediate()
        {
            return View();
        }
        public IActionResult Advanced()
        {
            return View();
        }
        public IActionResult Mastery()
        {
            return View();
        }
        public IActionResult Text1()
        {
            return View();
        }
        public IActionResult Text2()
        {
            return View();
        }

        public IActionResult Text3()
        {
            return View();
        }
        public IActionResult Text4()
        {
            return View();
        }
    }
}
