namespace ZyphCare.Web.Core.Constants;

/// <summary>
/// Contains constant URL values used across the application.
/// </summary>
public class UrlConstants
{
    /// <summary>
    /// Specifies the URL used for Auth0 login authentication within the application.
    /// This URL includes parameters required for authorization, such as response type, client ID,
    /// redirect URI, scope, and audience.
    /// </summary>
    public const string Auth0Login = "https://dev-g2gar2vb3zazyj0s.us.auth0.com/authorize?response_type=code&client_id=uYjZgtRicwlVkRR6WW9N3DqHw8C7EmAT&redirect_uri=https://localhost:7184/login&scope=offline_access&audience=https://zyphcare.com/authentication";
}