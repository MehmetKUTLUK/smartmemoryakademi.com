using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using crypto;

namespace Education.Data;

public class AppUser : IdentityUser<Guid>
{

    public required string Name { get; set; }
    public required string PromoCode { get; set; }
    public required string  SecurityQuestion { get; set; }

    public string? City { get; set; }

  
    public string? District { get; set; }
    public virtual ICollection<Category> CreatedCategories { get; set; } = new HashSet<Category>();
    public virtual ICollection<Course> CreatedCourses { get; set; } = new HashSet<Course>();
  


}
public class AppUserTypeConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder
            .HasIndex(x => x.Name)
            .IsUnique(false);
        builder
            .HasMany(x => x.CreatedCategories)
            .WithOne(x => x.CreatorUser)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(x => x.CreatedCourses)
            .WithOne(x => x.CreatorUser)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
       
    }
}
