using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ZyphCare.EntityFramework.Common;

namespace ZyphCare.EntityFramework.Sqlite;

/// <summary>
/// Contains extension methods for <see cref="DbContextOptionsBuilder"/>.
/// </summary>
public static class DbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Configures the <see cref="DbContextOptionsBuilder"/> to use ZyphCare's SQLite configuration,
    /// including optional migration assembly, connection string, custom ZyphCare-specific options, and
    /// additional SQLite-specific configuration.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="DbContextOptionsBuilder"/> to configure.
    /// </param>
    /// <param name="migrationsAssembly">
    /// The <see cref="Assembly"/> to be used for migrations.
    /// </param>
    /// <param name="connectionString">
    /// The connection string for the SQLite database. Default is "Data Source=zyphcare.sqlite.db;Cache=Shared;".
    /// </param>
    /// <param name="options">
    /// Optional <see cref="ZyphCareDbContextOptions"/> for further customization of the DbContext.
    /// </param>
    /// <param name="configure">
    /// An optional action to further configure the <see cref="SqliteDbContextOptionsBuilder"/>.
    /// </param>
    /// <returns>
    /// The configured <see cref="DbContextOptionsBuilder"/> instance.
    /// </returns>
    public static DbContextOptionsBuilder UseZyphCareSqlite(this DbContextOptionsBuilder builder, Assembly migrationsAssembly, string connectionString = "Data Source=ZyphCare.sqlite.db;Cache=Shared;", ZyphCareDbContextOptions? options = default, Action<SqliteDbContextOptionsBuilder>? configure = default) =>
        builder
            .UseZyphCareDbContextOptions(options)
            .UseSqlite(connectionString, db =>
            {
                db
                    .MigrationsAssembly(options.GetMigrationsAssemblyName(migrationsAssembly))
                    .MigrationsHistoryTable(options.GetMigrationsHistoryTableName(), options.GetSchemaName());

                configure?.Invoke(db);
            });
}