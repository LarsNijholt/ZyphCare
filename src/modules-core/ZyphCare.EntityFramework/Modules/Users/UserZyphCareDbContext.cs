using Microsoft.EntityFrameworkCore;
using ZyphCare.EntityFramework.Common;
using ZyphCare.Users.Entities;

namespace ZyphCare.EntityFramework.Modules.Users;

/// <summary>
/// The database context for managing user entities.
/// </summary>
public class UserZyphCareDbContext : ZyphCareDbContextBase
{
    /// <inheritdoc />
    public UserZyphCareDbContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Represents a set of users.
    /// </summary>
    public DbSet<User> Users { get; set; } = default!;

    /// <inheritdoc />
    protected override void ApplyEntityConfigurations(ModelBuilder modelBuilder)
    {
        var configuration = new Configurations();
        modelBuilder.ApplyConfiguration(configuration);
    }
}