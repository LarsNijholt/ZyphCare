using Microsoft.EntityFrameworkCore;

namespace ZyphCare.EntityFramework.Common.Contracts;

/// <summary>
/// Defines a mechanism for handling database exceptions that occur during operations
/// with a specified Entity Framework Core DbContext.
/// </summary>
/// <typeparam name="TDbContext">
/// Specifies the type of the DbContext this handler is intended to work with.
/// Must inherit from Entity Framework Core's DbContext.
/// </typeparam>
public interface IDbExceptionHandler<TDbContext> where TDbContext : DbContext
{
    /// Handles the given exception that occurs during database operations.
    public void Handle(DbUpdateException exception);
}