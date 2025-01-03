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

    /// <summary>
    /// Configures the aspect. Can be overridden to implement custom configuration logic.
    /// </summary>
    void Configure();

    /// <summary>
    /// Configures the hosted services.
    /// </summary>
    void ConfigureHostedServices();

    /// <summary>
    /// Applies the aspect configuration and behavior. Intended to be overridden
    /// by derived types to provide custom logic for applying the aspect.
    /// </summary>
    void Apply();

}