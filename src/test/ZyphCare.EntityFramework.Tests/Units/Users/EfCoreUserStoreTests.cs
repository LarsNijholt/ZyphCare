using Microsoft.Data.Sqlite;

namespace ZyphCare.EntityFrameworkCore.Tests.Units.Users;

public sealed class EfCoreUserStoreTests : IDisposable
{
    private readonly SqliteConnection _connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfCoreUserStoreTests"/> class
    /// </summary>
    public EfCoreUserStoreTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
    }
    
    /// <inheritdoc />
    public void Dispose()
    {
        _connection.Dispose();
    }
}