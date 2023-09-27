using Education.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Education
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseEducation(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            context.Database.Migrate();
            new[]
        {
            new AppRole{ Name = "Administrators"},
             new AppRole{ Name = "Members"},

        }
        .ToList()
        .ForEach(role =>
        {
            if (!roleManager.RoleExistsAsync(role.Name).Result)
            {
                var result = roleManager.CreateAsync(role).Result;
            }
        });

            var user = new AppUser
            {
                Name = configuration.GetValue<string>("DefaultUser:Name"),
                PromoCode = configuration.GetValue<string>("DefaultUser:PromoCode"),
                UserName = configuration.GetValue<string>("DefaultUser:Email"),
                SecurityQuestion = configuration.GetValue<string>("DefaultUser:SecurityQuestion"),
                Email = configuration.GetValue<string>("DefaultUser:Email"),
                EmailConfirmed = true
            };

            userManager.CreateAsync(user, configuration.GetValue<string>("DefaultUser:Password")).Wait();
            userManager.AddToRoleAsync(user, "Administrators").Wait();
            userManager.AddClaimAsync(user, new Claim("Name", user.Name)).Wait();
            return app;
        }

    }

}
