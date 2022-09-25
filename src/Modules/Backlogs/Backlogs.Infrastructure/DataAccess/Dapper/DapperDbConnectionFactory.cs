using System.Data;
using BacklogOrganizer.Modules.Backlogs.Core.Data;
using BacklogOrganizer.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.Options;
using Npgsql;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Dapper;
public class DapperDbConnectionFactory : IQueryDbConnectionFactory
{
    private readonly string _connectionString;

    public DapperDbConnectionFactory(IOptions<PostgresOptions> postgresOptions)
    {
        _connectionString = postgresOptions.Value.ConnectionString;
    }

    public async Task<IDbConnection> CreateNewConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
