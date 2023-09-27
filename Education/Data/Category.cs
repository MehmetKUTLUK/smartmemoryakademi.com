using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Education.Data;

public class Category : BaseEntity
{
    [Display(Name = "Kategori Logo")]
    public string? Logo { get; set; }

    [Display(Name = "Kategori Logo")]
    [NotMapped]
    public IFormFile? LogoFile { get; set; }

    public virtual ICollection<Course> Courses { get; set; }= new HashSet<Course>();  

    
}
public class CategoryTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasIndex(x => x.Name)
            .IsUnique(false);
        builder
            .HasMany(x => x.Courses)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}