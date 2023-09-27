using Education.Data;
using Education.Migrations;
using Education.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Education.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration configuration;
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;

        public AccountController(
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
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult StudentLogin()
        {
            return View(new StundentLoginViewModel { IsPersistent = true });
        }
        [HttpPost]
        public async Task<IActionResult> StudentLogin(StundentLoginViewModel model)
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
        public IActionResult Login()
        {
            return View(new LoginViewModel { IsPersistent = true });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsPersistent, true);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı girişi!");
                return View(model);
            }


        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email, PromoCode = model.PromoCode, Name = model.Name, PhoneNumber = model.PhoneNumber, City = model.City, District = model.District, EmailConfirmed = true, SecurityQuestion = model.SecurityQuestion };
                List<string> codes = context.PromoCodes.Where(x => x.Enabled == true).Select(x => x.Code).ToList();

                bool varmı = codes.Any(x => x == model.PromoCode);
                if (varmı == true)
                {

                    var result = await userManager.CreateAsync(user, model.Password);

                    await userManager.AddToRoleAsync(user, "Members");
                    await userManager.AddClaimAsync(user, new Claim("Name", user.Name));
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        var promoCodeenab = model.PromoCode;
                        var item = await context.PromoCodes.FirstOrDefaultAsync(x => x.Code == model.PromoCode);
                        var promoCode = await context.PromoCodes.Where(x => x.Code == model.PromoCode).FirstOrDefaultAsync(x => string.IsNullOrEmpty(x.CodeUserName));


                        if (item != null)
                        {
                            item.Enabled = false;
                            context.PromoCodes.Update(item);
                            await context.SaveChangesAsync();
                        }
                        if (promoCode != null)
                        {
                            promoCode.CodeUserName = model.Name;
                            context.PromoCodes.Update(promoCode);
                            await context.SaveChangesAsync();
                        }



                        return Redirect(model.ReturnUrl ?? "/Account/StudentLogin");
                    }

                }
                else
                {
                    return View("Registerpromo");
                }
            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName && x.SecurityQuestion == model.SecurityQuestion);
            if (user is null)
            {
                ModelState.AddModelError("", "Geçersiz e-posta adresi!");
                return View(model);
            }
            return View("ResetPasswordForm", new ResetPasswordFormViewModel { UserId = user.Id });

        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordForm(ResetPasswordFormViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId.ToString());
            var result = await userManager.RemovePasswordAsync(user);
            var newResutl = await userManager.AddPasswordAsync(user,model.Password);
            if (result.Succeeded && newResutl.Succeeded)
                return View("ResetPasswordSuccess");

            ModelState.AddModelError("", "Geçersiz kod!");
            return View(model);
        }
    }
}