using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using ZyphCare.EntityFramework.Common.Contracts;

namespace ZyphCare.EntityFramework.Common;

/// <summary>
/// Extends the functionality of the MigrationsAssembly class to support schema-aware migration generation
/// for DbContext instances that implement the IZyphCareDbContextSchema interface. Ensures schema-specific
/// migrations are created dynamically when a schema is provided, while retaining the ability to fall back
/// to the default migration creation logic.
/// </summary>
public class DbSchemaAwareMigrationAssembly : MigrationsAssembly
{
    private readonly DbContext _context;

    /// <inheritdoc />
    public DbSchemaAwareMigrationAssembly(ICurrentDbContext currentContext,
        IDbContextOptions options, IMigrationsIdGenerator idGenerator,
        IDiagnosticsLogger<DbLoggerCategory.Migrations> logger)
        : base(currentContext, options, idGenerator, logger)
    {
        _context = currentContext.Context;
    }

    /// <inheritdoc />
    public override Migration CreateMigration(TypeInfo migrationClass,
        string activeProvider)
    {
        if (activeProvider == null)
            throw new ArgumentNullException(nameof(activeProvider));

        var hasCtorWithSchema = migrationClass
            .GetConstructor(new[] { typeof(IZyphCareDbContextSchema) }) != null;

        if (hasCtorWithSchema && _context is IZyphCareDbContextSchema schema)
        {
            var instance = (Migration)Activator.CreateInstance(migrationClass.AsType(), schema);
            instance.ActiveProvider = activeProvider;
            return instance;
        }

        return base.CreateMigration(migrationClass, activeProvider);
    }
}