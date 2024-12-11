using Microsoft.EntityFrameworkCore;
using ZyphCare.EntityFramework.Common.Contracts;
using ZyphCare.EntityFramework.Modules.Users;

namespace ZyphCare.EntityFramework.Handlers;

/// <summary>
/// A Database exception handler that rethrows the original exception.
/// </summary>
public class RethrowDbExceptionHandler
    : IDbExceptionHandler<UserZyphCareDbContext>
        
{
    /// <summary>
    /// Rethrow the given exception that occurs during the database operations.
    /// </summary>
    /// <param name="exception"></param>
    public void Handle(DbUpdateException exception)
    {
        throw exception;
    }
}