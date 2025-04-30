namespace ZyphCare.EntityFramework.Common.Entities;

/// <summary>
/// An entity that supports versioning.
/// </summary>
public abstract class VersionedEntity
{
    /// <summary>
    /// The Id of this entity, which is unique per version.
    /// </summary>
    public string Id { get; set; } = default!;
    
    /// <summary>
    /// The id of this entity shared across versions.
    /// </summary>
    public string VersionId { get; set; }
}