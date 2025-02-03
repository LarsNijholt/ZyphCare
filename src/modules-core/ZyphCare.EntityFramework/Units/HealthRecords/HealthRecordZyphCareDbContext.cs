using Microsoft.EntityFrameworkCore;
using ZyphCare.EntityFramework.Common;
using ZyphCare.HealthRecords.Entities;

namespace ZyphCare.EntityFramework.Units.HealthRecords;

/// <summary>
/// The database context for managing health record entities.
/// </summary>
public class HealthRecordZyphCareDbContext : ZyphCareDbContextBase
{
    /// <inheritdoc />
    public HealthRecordZyphCareDbContext(DbContextOptions options) : base(options)
    {
    }
    
    /// <summary>
    /// Represents a set of health records.
    /// </summary>
    public DbSet<HealthRecord> HealthRecords { get; set; } = default!;
    
    /// <inheritdoc />
    protected override void ApplyEntityConfigurations(ModelBuilder modelBuilder)
    {
        var configuration = new Configurations();
        modelBuilder.ApplyConfiguration(configuration);
    }
}