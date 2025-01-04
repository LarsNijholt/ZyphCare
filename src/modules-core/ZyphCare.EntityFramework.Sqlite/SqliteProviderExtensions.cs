using System.Reflection;
using Microsoft.VisualBasic;
using ZyphCare.EntityFramework.Common;

namespace ZyphCare.EntityFramework.Sqlite;

/// <summary>
/// Contains extension methods for configuring SQLite as the database provider
/// for the EF Core persistence aspect in a ZyphCare application.
/// </summary>
public static class SqliteProviderExtensions
{
    private static Assembly Assembly => typeof(SqliteProviderExtensions).Assembly;

}