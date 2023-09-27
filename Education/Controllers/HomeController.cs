using Education.Data;
using Education.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System.Diagnostics;

namespace Education.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        
        private readonly IConfiguration configuration;
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;

        public HomeController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
           
             IConfiguration configuration,
                AppDbContext context,
            IWebHostEnvironment env

            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
           
            this.context = context;
            this.env = env;

        }




        public IActionResult StudentLogin()
        {
            return View(new StundentLoginViewModel { IsPersistent = true });
        }



        public IActionResult Login()
        {
            return View(new LoginViewModel { IsPersistent = true });
        }
        public IActionResult Index()
        {
            return View(new StundentLoginViewModel { IsPersistent = true });
        }
        [HttpPost]
        public async Task<IActionResult> Index(StundentLoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsPersistent, true);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "/Student");
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı girişi!");
                return View(model);
            }
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
    }
}

