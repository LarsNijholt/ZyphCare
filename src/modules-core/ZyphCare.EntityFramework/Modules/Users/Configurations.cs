using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZyphCare.Users.Entities;

namespace ZyphCare.EntityFramework.Modules.Users;

/// <summary>
/// EF core configuration for the various sets of <see cref="DbContext"/>.
/// </summary>
public class Configurations : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => new { x.Auth0Id }).HasDatabaseName($"IX_{nameof(User)}_{nameof(User.Auth0Id)}").IsUnique();
        builder.HasIndex(x => x.Role).HasDatabaseName($"IX_{nameof(User)}_{nameof(User.Role)}");
    }
}