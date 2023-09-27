using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Data;

public class BaseEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public virtual AppUser? CreatorUser { get; set; }
    public Guid UserId { get; set; }
    public virtual DateTime DateCreated { get; set; } = DateTime.UtcNow;
}


