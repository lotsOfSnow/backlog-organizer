using BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Dapper;
public class PostgresQueryRepository : IQueryRepository
{
    private readonly IDbQueryCreator<PostgresQuery> _queryCreator;

    public PostgresQueryRepository(IDbQueryCreator<PostgresQuery> queryCreator, IQueryDbConnectionFactory connectionFactory)
    {
        _queryCreator = queryCreator;
        ConnectionFactory = connectionFactory;
    }

    public IQueryDbConnectionFactory ConnectionFactory { get; }

    public string GetQuery(string format, params string[] argumentValues)
        => _queryCreator.Create(format, argumentValues).Value;
}
