using FastEndpoints;

namespace ZyphCare.Api.Common.Abstractions;

/// <inheritdoc />
public abstract class ZyphCareEndpointWithMapper<TRequest, TMapper> : EndpointWithMapper<TRequest, TMapper> where TMapper : class, IRequestMapper where TRequest : notnull
{
    /// <summary>
    /// Configures the permissions for the endpoint.
    /// </summary>
    /// <param name="permissions">
    /// An array of permission strings to be configured for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access.
    /// Otherwise, the provided permissions will be combined with the default "All" permission.
    /// </param>
    protected void ConfigurePermissions(params string[] permissions)
    {
        if (!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Permissions(new[] { PermissionNames.All }.Concat(permissions).ToArray());
    }
    
    /// <summary>
    /// Configures the roles required to access the endpoint.
    /// </summary>
    /// <param name="roles">
    /// An array of role names to be configured for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access.
    /// Otherwise, only users belonging to the specified roles will have access.
    /// </param>
    protected void ConfigureRoles(params string[] roles)
    {
        if(!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Roles(roles);
    }
}

/// <inheritdoc />
public abstract class ZyphCareEndpointWithoutRequest : EndpointWithoutRequest
{
    /// <summary>
    /// Configures the permissions for the endpoint.
    /// </summary>
    /// <param name="permissions">
    /// An array of permission strings to be configured for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access.
    /// Otherwise, the provided permissions will be combined with the default "All" permission.
    /// </param>
    protected void ConfigurePermissions(params string[] permissions)
    {
        if (!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Permissions(new[] { PermissionNames.All }.Concat(permissions).ToArray());
    }
    
    /// <summary>
    /// Configures the roles required to access the endpoint.
    /// </summary>
    /// <param name="roles">
    /// An array of role names to be configured for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access.
    /// Otherwise, only users belonging to the specified roles will have access.
    /// </param>
    protected void ConfigureRoles(params string[] roles)
    {
        if(!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Roles(roles);
    }
}

/// <inheritdoc />
public abstract class ZyphCareEndpointWithoutRequest<TResponse> : EndpointWithoutRequest<TResponse> where TResponse : notnull
{
    /// <summary>
    /// Configures the permissions for the endpoint.
    /// </summary>
    /// <param name="permissions">
    /// An array of permissions to be associated with the endpoint. If security is disabled, the
    /// endpoint allows anonymous access. Otherwise, the provided permissions are combined with the
    /// default "All" permission.
    /// </param>
    protected void ConfigurePermissions(params string[] permissions)
    {
        if (!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Permissions(new[] { PermissionNames.All }.Concat(permissions).ToArray());
    }
    
    /// <summary>
    /// Configures the roles required to access the endpoint.
    /// </summary>
    /// <param name="roles">
    /// An array of role names to be configured for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access.
    /// Otherwise, only users belonging to the specified roles will have access.
    /// </param>
    protected void ConfigureRoles(params string[] roles)
    {
        if(!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Roles(roles);
    }
}

/// <inheritdoc />
public class ZyphCareEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull, new() where TResponse : notnull
{
    /// <summary>
    /// Configures the permissions for the endpoint.
    /// </summary>
    /// <param name="permissions">
    /// An array of permission strings to be configured for the endpoint.
    /// When security is disabled, the endpoint will allow anonymous access.
    /// If security is enabled, the specified permissions will be combined with the default "All" permission.
    /// </param>
    protected void ConfigurePermissions(params string[] permissions)
    {
        if (!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Permissions(new[] { PermissionNames.All }.Concat(permissions).ToArray());
    }

    /// <summary>
    /// Configures the roles required to access the endpoint.
    /// </summary>
    /// <param name="roles">
    /// An array of role names to be configured for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access.
    /// Otherwise, only users belonging to the specified roles will have access.
    /// </param>
    protected void ConfigureRoles(params string[] roles)
    {
        if(!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Roles(roles);
    }
}

/// <inheritdoc />
public class ZyphCareEndpoint<TRequest, TResponse, TMapper> : Endpoint<TRequest, TResponse, TMapper> where TRequest : notnull, new() where TResponse : notnull where TMapper : class, IMapper, new()
{
    /// <summary>
    /// Configures the permissions required to access the endpoint.
    /// </summary>
    /// <param name="permissions">
    /// A collection of permission strings that define access control for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access. Otherwise,
    /// the provided permissions are combined with the default "All" permission for access configuration.
    /// </param>
    protected void ConfigurePermissions(params string[] permissions)
    {
        if (!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Permissions(new[] { PermissionNames.All }.Concat(permissions).ToArray());
    }
    
    /// <summary>
    /// Configures the roles required to access the endpoint.
    /// </summary>
    /// <param name="roles">
    /// An array of role names to be configured for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access.
    /// Otherwise, only users belonging to the specified roles will have access.
    /// </param>
    protected void ConfigureRoles(params string[] roles)
    {
        if(!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Roles(roles);
    }
}

/// <inheritdoc />
public class ZyphCareEndpoint<TRequest> : Endpoint<TRequest> where TRequest : notnull, new()
{
    /// <summary>
    /// Configures the permissions for the endpoint.
    /// </summary>
    /// <param name="permissions">
    /// An array of permission strings to be configured for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access.
    /// Otherwise, the provided permissions will be combined with the default "All" permission.
    /// </param>
    protected void ConfigurePermissions(params string[] permissions)
    {
        if (!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Permissions(new[] { PermissionNames.All }.Concat(permissions).ToArray());
    }

    /// <summary>
    /// Configures the roles required to access the endpoint.
    /// </summary>
    /// <param name="roles">
    /// An array of role names to be configured for the endpoint.
    /// If security is disabled, the endpoint will allow anonymous access.
    /// Otherwise, only users belonging to the specified roles will have access.
    /// </param>
    protected void ConfigureRoles(params string[] roles)
    {
        if(!EndpointSecurityOptions.SecurityIsEnabled)
            AllowAnonymous();
        else
            Roles(roles);
    }
}