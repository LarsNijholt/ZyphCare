﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Abstractions;
using ZyphCare.Aspects.Contracts;
using ZyphCare.EntityFramework.Common.Entities;

namespace ZyphCare.EntityFramework.Common;

/// <summary>
///     Base class for features that require Entity Framework Core.
/// </summary>
/// <typeparam name="TDbContext">The type of the database context.</typeparam>
public abstract class PersistenceAspectBase<TDbContext> : BaseAspect where TDbContext : DbContext
{
    public Action<IServiceProvider, DbContextOptionsBuilder> DbContextOptionsBuilder = (_, options) => options
        .UseZyphCareDbContextOptions(default)
        .UseSqlite("Data Source=zyphcare.sqlite.db;Cache=Shared;", sqlite => sqlite
            .MigrationsAssembly("ZyphCare.EntityFrameworkCore.Sqlite")
            .MigrationsHistoryTable(ZyphCareDbContextBase.MigrationsHistoryTable, ZyphCareDbContextBase.ZyphCareSchema));

    /// <inheritdoc />
    protected PersistenceAspectBase(IUnit module) : base(module)
    {
    }

    /// <summary>
    ///     Gets or sets a value indicating whether to use context pooling.
    /// </summary>
    public bool UseContextPooling { get; set; } = false;

    /// <summary>
    ///     Gets or sets a value indicating whether to run migrations.
    /// </summary>
    public bool RunMigrations { get; set; } = true;

    /// <summary>
    ///     Gets or sets the lifetime of the <see cref="IDbContextFactory{TContext}" />. Defaults to
    ///     <see cref="ServiceLifetime.Singleton" />.
    /// </summary>
    public ServiceLifetime DbContextFactoryLifetime { get; set; } = ServiceLifetime.Scoped;

    /// <inheritdoc />
    public override void ConfigureHostedServices()
    {
        if (RunMigrations)
            Unit.ConfigureHostedService<RunMigrationsHostedService<TDbContext>>(
                -100); // Migrations need to run before other hosted services that depend on DB access.
    }

    /// <inheritdoc />
    public override void Apply()
    {
        if (UseContextPooling)
            Services.AddPooledDbContextFactory<TDbContext>(DbContextOptionsBuilder);
        else
            Services.AddDbContextFactory<TDbContext>(DbContextOptionsBuilder, DbContextFactoryLifetime);
    }

    /// <summary>
    /// Add an entity store to the service collection.
    /// </summary>
    /// <typeparam name="TEntity">The entity the store is for.</typeparam>
    /// <typeparam name="TStore">the entity store.</typeparam>
    protected void AddEntityStore<TEntity, TStore>() where TEntity : Entity, new() where TStore : class
    {
        Services
            .AddScoped<EntityStore<TDbContext, TEntity>>()
            .AddScoped<TStore>();
    }
}