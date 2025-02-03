using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZyphCare.HealthRecords.Entities;

namespace ZyphCare.EntityFramework.Units.HealthRecords;

/// <summary>
/// Provides the configuration for the HealthRecord entity within the Entity Framework model.
/// Manages entity indexes to optimize query performance on specific properties.
/// </summary>
public class Configurations : IEntityTypeConfiguration<HealthRecord>
{

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<HealthRecord> builder)
    {
        builder.HasIndex(x => new { x.FileName }).HasDatabaseName($"IX_{nameof(HealthRecord)}_{nameof(HealthRecord.FileName)}");
        builder.HasIndex(x => new { x.Type }).HasDatabaseName($"IX_{nameof(HealthRecord)}_{nameof(HealthRecord.Type)}");
        builder.HasIndex(x => new {x.CreatedDate}).HasDatabaseName($"IX_{nameof(HealthRecord)}_{nameof(HealthRecord.CreatedDate)}");
    }
}