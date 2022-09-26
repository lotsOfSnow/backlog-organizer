using BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Dapper;
public class PostgresDbQueryCreator : IDbQueryCreator<PostgresQuery>
{
    public PostgresQuery Create(string format, params string[] argumentsValues)
        => new(format, argumentsValues);
}
