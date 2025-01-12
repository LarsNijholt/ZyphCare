using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZyphCare.Users.Entities;

namespace ZyphCare.EntityFramework.Units.Users;

/// <summary>
/// EF core configuration for the various sets of <see cref="DbContext"/>.
/// </summary>
public class Configurations : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => new { x.Auth0Id }).HasDatabaseName($"IX_{nameof(User)}_{nameof(User.Auth0Id)}").IsUnique();
        builder.HasIndex(x => new { x.FirstName }).HasDatabaseName($"IX_{nameof(User)}_{nameof(User.FirstName)}");
        builder.HasIndex(x => new { x.LastName }).HasDatabaseName($"IX_{nameof(User)}_{nameof(User.LastName)}");
        builder.HasIndex(x => new { x.BloodType }).HasDatabaseName($"IX_{nameof(User)}_{nameof(User.BloodType)}");

    }
}