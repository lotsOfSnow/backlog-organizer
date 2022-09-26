using System.Data;
using BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
using BacklogOrganizer.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.Options;
using Npgsql;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Dapper;
public class PostgresQueryDbConnectionFactory : IQueryDbConnectionFactory, IDisposable
{
    private readonly string _connectionString;
    private readonly IDbQueryCreator<PostgresQuery> _queryCreator;

    private IDbConnection? _connection;

    public PostgresQueryDbConnectionFactory(IOptions<PostgresOptions> postgresOptions, IDbQueryCreator<PostgresQuery> queryCreator)
    {
        _connectionString = postgresOptions.Value.ConnectionString;
        _queryCreator = queryCreator;
    }

    public async Task<IDbConnection> GetOrCreateConnectionAsync()
    {
        if (_connection?.State == ConnectionState.Open)
        {
            return _connection;
        }

        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        _connection = connection;

        return connection;
    }

    public string BuildQuery(string format, params string[] argumentValues)
        => _queryCreator.Create(format, argumentValues).Value;

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
