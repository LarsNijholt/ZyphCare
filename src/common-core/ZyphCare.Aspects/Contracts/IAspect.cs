namespace ZyphCare.Aspects.Contracts;

/// <summary>
/// Represents an aspect that provides reusable functionality or behavior to be applied
/// across different components or services. Acts as a marker interface for such behaviors.
/// </summary>
public interface IAspect
{
    /// <summary>
    /// Represents a unit within the system, providing services and properties that can
    /// be used to manage and interact with various functionalities. The Unit property
    /// serves as a bridge between aspects and their corresponding behaviors or services.
    /// </summary>
    IUnit Unit { get; }

    void Configure();

    void Apply();

}