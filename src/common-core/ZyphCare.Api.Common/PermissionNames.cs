namespace ZyphCare.Api.Common;

/// <summary>
/// Provides a set of standard permission names used to define access levels
/// for various endpoints in the application. These constants can be used
/// to configure security and assign appropriate permissions.
/// </summary>
public static class PermissionNames
{
    /// <summary>
    /// Represents a wildcard permission used to grant unrestricted access
    /// within the application. The "All" permission can be utilized to
    /// signify that any user with this permission is allowed to access all
    /// endpoints or resources associated with it.
    /// </summary>
    public const string All = "*";
}