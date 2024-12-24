namespace ZyphCare.Api.Common;

/// <summary>
/// Represents configuration options related to endpoint security within the application.
/// </summary>
/// <remarks>
/// This class contains static options and methods to configure security settings for application endpoints.
/// The options specify role names and allow enabling or disabling security globally.
/// </remarks>
public class EndpointSecurityOptions
{
    /// <summary>
    /// Represents the role name for users with administrative permissions in the application.
    /// </summary>
    /// <remarks>
    /// This static field is used to specify the role name associated with granting
    /// full administrative access to manage and configure various application functionalities or resources.
    /// </remarks>
    public static string AdminRoleName = "Admin";

    /// <summary>
    /// Represents the role name for users with read-only permissions in the application.
    /// </summary>
    /// <remarks>
    /// This static field is used to specify the role name associated with granting
    /// read-level access to view but not modify various application functionalities or resources.
    /// </remarks>
    public static string ReaderRoleName = "Reader";

    /// <summary>
    /// Represents the role name for users with write permissions in the application.
    /// </summary>
    /// <remarks>
    /// This static field is used to specify the role name associated with granting
    /// write-level access to various application functionalities or resources.
    /// </remarks>
    public static string WriteRoleName = "Writer";

    /// <summary>
    /// Determines whether the global security settings for application endpoints are enabled or disabled.
    /// </summary>
    /// <remarks>
    /// When set to <c>true</c>, security measures such as role-based permissions are enforced on the application endpoints.
    /// When set to <c>false</c>, security is globally disabled, allowing unrestricted access to all endpoints.
    /// This setting is critical for managing the application's security posture and should be adjusted with caution.
    /// </remarks>
    public static bool SecurityIsEnabled = true;

    /// <summary>
    /// Disables the global security settings for application endpoints.
    /// </summary>
    /// <remarks>
    /// This method sets the <see cref="EndpointSecurityOptions.SecurityIsEnabled"/> flag to <c>false</c>,
    /// effectively turning off all security features that rely on this property.
    /// Use with caution, as disabling security globally might expose sensitive data or actions to unauthorized users.
    /// </remarks>
    public static void DisableSecurity() => SecurityIsEnabled = false;
}