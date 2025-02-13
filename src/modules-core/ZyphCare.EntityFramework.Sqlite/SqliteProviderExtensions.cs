using System.Reflection;
using Microsoft.VisualBasic;
using ZyphCare.EntityFramework.Common;
using ZyphCare.EntityFramework.Units.HealthRecords;
using ZyphCare.EntityFramework.Units.Users;

namespace ZyphCare.EntityFramework.Sqlite;

/// <summary>
/// Contains extension methods for configuring SQLite as the database provider
/// for the EF Core persistence aspect in a ZyphCare application.
/// </summary>
public static class SqliteProviderExtensions
{
    private static Assembly Assembly => typeof(SqliteProviderExtensions).Assembly;

    /// <summary>
    /// Configures the persistence aspect to use SQLite as the database provider.
    /// </summary>
    /// <param name="aspect">The persistence aspect being configured.</param>
    /// <param name="connectionString">The SQLite connection string. Defaults to "Data Source=zyphcare.sqlite.db;Cache=Shared;".</param>
    /// <param name="options">Optional database context configuration options.</param>
    /// <returns>Returns the configured <see cref="EfCoreUserPersistenceAspect"/> instance.</returns>
    public static EfCoreUserPersistenceAspect UseSqlite(this EfCoreUserPersistenceAspect aspect, string connectionString = "Data Source=ZyphCare.sqlite.db;Cache=Shared;", ZyphCareDbContextOptions? options = default)
    {
        aspect.DbContextOptionsBuilder = (_, db) => db.UseZyphCareSqlite(Assembly, connectionString, options);
        return aspect;
    }

    /// <summary>
    /// Configures the health records persistence aspect to use SQLite as the database provider.
    /// </summary>
    /// <param name="aspect">The health record persistence aspect being configured.</param>
    /// <param name="connectionString">The SQLite connection string. Defaults to "Data Source=ZyphCare.sqlite.db;Cache=Shared;".</param>
    /// <param name="options">Optional database context configuration options.</param>
    /// <returns>Returns the configured <see cref="HealthRecordPersistenceAspect"/> instance.</returns>
    public static HealthRecordPersistenceAspect UseSqlite(this HealthRecordPersistenceAspect aspect, string connectionString = "Data Source=ZyphCare.sqlite.db;Cache=Shared;", ZyphCareDbContextOptions? options = default)
    {
        aspect.DbContextOptionsBuilder = (_, db) => db.UseZyphCareSqlite(Assembly, connectionString, options);
        return aspect;
    }
}