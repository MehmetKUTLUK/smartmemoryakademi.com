using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Education.Data;

public class Course : BaseEntity
{

    [Display(Name = "kurs video")]
    [NotMapped]
    public IFormFile? CourseVideoFile { get; set; }
    public string? VideoUrl { get; set; }   

    [Display(Name = "Kategori Logo")]
    [NotMapped]
    public IFormFile? LogoFile { get; set; }
    public required string CourseLogo { get; set; }
    public required string VideoName { get; set; }
    public string? Description { get; set; }
    public string? CourseDuration { get; set; }
    public virtual Category?  Category { get; set; }
    public Guid  CategoryId { get; set; }

   
}
public class CourseTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder
            .HasIndex(x => x.Name)
            .IsUnique(false);
        builder
              .Property(x => x.VideoName)
              .IsRequired();
        builder
            .Property(x => x.CourseLogo)
            .IsRequired();
       
    }
}