using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Education.Data;

public class PromoCode
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Enabled { get; set; } = true;
    
    public Guid? UserId { get; set; }
    public string? CodeUserName { get; set; }

}
public class CodeGeneratorTypeConfiguration : IEntityTypeConfiguration<PromoCode>
{
    public void Configure(EntityTypeBuilder<PromoCode> builder)
    {


    }
}