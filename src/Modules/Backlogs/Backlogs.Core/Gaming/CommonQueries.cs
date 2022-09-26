using System.Data;
using BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;
using BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
using Dapper;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;

public static class CommonQueries
{
    public static async Task<bool> BacklogExists(IQueryRepository queryRepository, IDbConnection dbConnection, Guid id)
    {
        var queryArgs = new { id };
        var query = queryRepository.GetExistenceCheckQuery(OrmMappings.Backlogs.Table, OrmMappings.Backlogs.Columns.Id, nameof(id));
        return await dbConnection.QuerySingleOrDefaultAsync<bool>(query, queryArgs);
    }
}
