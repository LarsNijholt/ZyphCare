using System.Reflection;
using Microsoft.VisualBasic;
using ZyphCare.EntityFramework.Common;
using ZyphCare.EntityFramework.Modules.Users;

namespace ZyphCare.EntityFramework.Sqlite;

/// <summary>
/// Contains extension methods for configuring SQLite as the database provider
/// for the EF Core persistence feature in a ZyphCare application.
/// </summary>
public static class SqliteProviderExtensions
{
    private static Assembly Assembly => typeof(SqliteProviderExtensions).Assembly;

    /// <summary>
    /// Configures the persistence feature to use SQLite as the database provider.
    /// </summary>
    /// <param name="feature">The persistence feature being configured.</param>
    /// <param name="connectionString">The SQLite connection string. Defaults to "Data Source=zyphcare.sqlite.db;Cache=Shared;".</param>
    /// <param name="options">Optional database context configuration options.</param>
    /// <returns>Returns the configured <see cref="EfCoreUserPersistenceFeature"/> instance.</returns>
    public static EfCoreUserPersistenceFeature UseSqlite(this EfCoreUserPersistenceFeature feature, string connectionString = "Data Source=ZyphCare.sqlite.db;Cache=Shared;", ZyphCareDbContextOptions? options = default)
    {
        feature.DbContextOptionsBuilder = (_, db) => db.UseZyphCareSqlite(Assembly, connectionString, options);
        return feature;
    }
}