using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Education.Data;


    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public virtual DbSet<PromoCode> PromoCodes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<TemplateSetting> TemplateSettings { get; set; }
    public virtual DbSet<Course> Courses { get; set; }

       
    }


