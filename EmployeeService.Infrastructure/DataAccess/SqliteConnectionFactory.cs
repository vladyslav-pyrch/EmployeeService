using System.Data;
using EmployeeService.Common.Application.Data;
using Microsoft.Data.Sqlite;

namespace EmployeeService.Infrastructure.DataAccess;

public class SqliteConnectionFactory : ISqlConnectionFactory, IDisposable
{
    private readonly string _connectionString;
    private IDbConnection? _connection;

    public SqliteConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection OpenConnection
    {
        get
        {
            _connection ??= new SqliteConnection(_connectionString);
            
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            return _connection;
        }
    }

    public void Dispose()
    {
        if (_connection is { State: ConnectionState.Open})
            _connection.Dispose();
    }
}