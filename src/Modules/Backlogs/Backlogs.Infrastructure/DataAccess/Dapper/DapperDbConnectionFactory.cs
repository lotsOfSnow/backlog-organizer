using System.Data;
using BacklogOrganizer.Modules.Backlogs.Core.Data;
using BacklogOrganizer.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.Options;
using Npgsql;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Dapper;
public class DapperDbConnectionFactory : IQueryDbConnectionFactory, IDisposable
{
    private readonly string _connectionString;

    private IDbConnection? _connection;

    public DapperDbConnectionFactory(IOptions<PostgresOptions> postgresOptions)
    {
        _connectionString = postgresOptions.Value.ConnectionString;
    }

    public async Task<IDbConnection> CreateNewConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        _connection = connection;

        return connection;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_connection is null)
        {
            return;
        }

        if (disposing)
        {
            _connection.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
