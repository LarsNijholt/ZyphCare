namespace ZyphCare.Web.Identity.Options;

/// <summary>
/// Represents configuration options for ZyphCare's identity system.
/// This includes settings used for authentication and other identity-related operations.
/// </summary>
public class ZyphCareIdentityOptions
{
    /// <summary>
    /// A secret key used for secure operations within the ZyphCare Identity system.
    /// Typically utilized for authentication and authorization-related processes where a
    /// client secret is required, such as in obtaining access tokens.
    /// </summary>
    public string? Secret { get; set; } = default!;
}