using System.Collections.Concurrent;
using ZyphCare.Common.Entities;

namespace ZyphCare.EntityFramework.Common.Services;

/// <summary>
/// A class representing an in-memory storage mechanism for entities.
/// Provides functionality for adding, saving, querying, and managing entities in memory.
/// </summary>
/// <typeparam name="TEntity">The type of entity to store and manage.</typeparam>
public class MemoryStore<TEntity>
{
    private IDictionary<string, TEntity> Entities { get; } = new ConcurrentDictionary<string, TEntity>();

    /// <summary>
    /// Adds an entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="idAccessor">A function that returns the ID of the entity.</param>
    public void Add(TEntity entity, Func<TEntity, string> idAccessor)
    {
        Entities.Add(idAccessor(entity), entity);
    }

    /// <summary>
    /// Adds multiple entities.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    /// <param name="idAccessor">A function that returns the ID of an entity.</param>
    public void AddMany(IEnumerable<TEntity> entities, Func<TEntity, string> idAccessor)
    {
        foreach (var entity in entities)
            Add(entity, idAccessor);
    }

    /// <summary>
    /// Saves an entity by adding or updating it in the memory store.
    /// </summary>
    /// <param name="entity">The entity to be saved.</param>
    /// <param name="idAccessor">A function to determine the ID of the entity.</param>
    public void Save(TEntity entity, Func<TEntity, string> idAccessor)
    {
        Entities[idAccessor(entity)] = entity;
    }

    /// <summary>
    /// Saves multiple entities by adding or updating them in the store.
    /// </summary>
    /// <param name="entities">The collection of entities to save.</param>
    /// <param name="idAccessor">A function that retrieves the unique identifier for each entity.</param>
    public void SaveMany(IEnumerable<TEntity> entities, Func<TEntity, string> idAccessor)
    {
        foreach (var entity in entities)
            Save(entity, idAccessor);
    }

    /// <summary>
    /// Finds an entity matching the specified predicate.
    /// </summary>
    /// <param name="predicate">A function that defines the matching criteria for the entity.</param>
    /// <returns>The entity that matches the predicate, or null if no match is found.</returns>
    public TEntity? Find(Func<TEntity, bool> predicate)
    {
        return Entities.Values.Where(predicate).FirstOrDefault();
    }

    /// <summary>
    /// Finds all entities matching the specified predicate.
    /// </summary>
    /// <param name="predicate">A function to test each entity for a condition.</param>
    /// <returns>An enumerable collection of entities that match the specified predicate.</returns>
    public IEnumerable<TEntity> FindMany(Func<TEntity, bool> predicate)
    {
        return Entities.Values.Where(predicate);
    }

    /// <summary>
    /// Finds all entities that match the specified predicate and orders them by a specified key in the specified direction.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used for ordering.</typeparam>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="orderBy">A function that specifies the key for ordering the entities.</param>
    /// <param name="orderDirection">The direction in which the result should be ordered.</param>
    /// <returns>The collection of entities that match the predicate, ordered as specified.</returns>
    public IEnumerable<TEntity> FindMany<TKey>(Func<TEntity, bool> predicate, Func<TEntity, TKey> orderBy,
        OrderDirection orderDirection = OrderDirection.Ascending)
    {
        var query = Entities.Values.Where(predicate);

        query = orderDirection switch
            {
                OrderDirection.Ascending => query.OrderBy(orderBy),
                OrderDirection.Descending => query.OrderByDescending(orderBy),
                _ => query.OrderBy(orderBy)
        };

        return query;
    }

    /// <summary>
    /// Lists all entities stored in the memory store.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    public IEnumerable<TEntity> List()
    {
        return Entities.Values;
    }

    /// <summary>
    /// Deletes an entity by ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>True if the entity was deleted, otherwise false.</returns>
    public bool Delete(string id)
    {
        return Entities.Remove(id);
    }

    /// <summary>
    /// Deletes all entities matching the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to evaluate entities for deletion.</param>
    /// <returns>The number of entities deleted.</returns>
    public long DeleteWhere(Func<TEntity, bool> predicate)
    {
        var query =
            from entry in Entities
            where predicate(entry.Value)
            select entry;

        var entries = query.ToList();
        foreach (var entry in entries)
            Entities.Remove(entry);

        return entries.LongCount();
    }

    /// <summary>
    /// Deletes all entities matching the specified IDs.
    /// </summary>
    /// <param name="ids">The IDs of the entities to delete.</param>
    /// <returns>The number of entities deleted.</returns>
    public long DeleteMany(IEnumerable<string> ids)
    {
        var count = 0;
        foreach (var id in ids)
        {
            count++;
            Entities.Remove(id);
        }

        return count;
    }

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="idAccessor">A function that returns the ID of the entity.</param>
    /// <returns>The number of entities that were deleted.</returns>
    public long DeleteMany(IEnumerable<TEntity> entities, Func<TEntity, string> idAccessor)
    {
        var count = 0;
        var list = entities.ToList();

        foreach (var entity in list)
        {
            count++;
            var id = idAccessor(entity);
            Entities.Remove(id);
        }

        return count;
    }

    /// <summary>
    /// Executes a query on the stored entities and returns the result.
    /// </summary>
    /// <param name="query">A function used to apply the query to the stored entities.</param>
    /// <returns>An enumeration of entities resulting from the query.</returns>
    public IEnumerable<TEntity> Query(Func<IQueryable<TEntity>, IQueryable<TEntity>> query)
    {
        var queryable = Entities.Values.AsQueryable();
        return query(queryable);
    }

    /// <summary>
    /// Returns true if any entity matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to evaluate against entities.</param>
    /// <returns>True if any entity matches the specified predicate; otherwise, false.</returns>
    public bool Any(Func<TEntity, bool> predicate)
    {
        return Entities.Values.Any(predicate);
    }

    /// <summary>
    /// Returns the count of entities matching the specified predicate, with an optional property selector for distinct counting.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="propertySelector">The property selector for distinct filtering.</param>
    /// <returns>The number of entities matching the specified predicate.</returns>
    public long Count<TProperty>(Func<TEntity, bool> predicate, Func<TEntity, TProperty> propertySelector)
    {
        return Entities.Values
            .DistinctBy(propertySelector)
            .Count(predicate);
    }
}