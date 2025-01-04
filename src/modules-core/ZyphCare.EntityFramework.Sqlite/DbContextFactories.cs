using Microsoft.EntityFrameworkCore;
using ZyphCare.EntityFramework.Common.Abstractions;

namespace ZyphCare.EntityFramework.Sqlite;



/// <summary>
/// A factory for creating instances of a specified <see cref="DbContext"/> implementation
/// derived from <see cref="DesignTimeDbContextFactoryBase{TDbContext}"/> for SQLite during design-time.
/// This factory is typically used by the EF Core tools, such as migrations, to create a DbContext
/// with a pre-configured SQLite database connection and necessary options.
/// </summary>
/// <typeparam name="TDbContext">
/// The type of the DbContext to be created by this factory. Must inherit from <see cref="DbContext"/>.
/// </typeparam>
public class SqliteDesignTimeDbContextFactory<TDbContext> : DesignTimeDbContextFactoryBase<TDbContext> where TDbContext : DbContext
{
    /// <inheritdoc />
    protected override void ConfigureBuilder(DbContextOptionsBuilder<TDbContext> builder, string connectionString)
    {
        builder.UseZyphCareSqlite(GetType().Assembly, connectionString);
    }
}