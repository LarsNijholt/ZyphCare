namespace ZyphCare.EntityFramework.Common.Entities;

/// <summary>
/// An entity that supports versioning.
/// </summary>
public abstract class VersionedEntity : Entity
{
    /// <summary>
    /// The id of this entity shared across versions.
    /// </summary>
    public string VersionId { get; set; }
}