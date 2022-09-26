using System.Data;

namespace BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
public interface IQueryDbConnectionFactory
{
    Task<IDbConnection> GetOrCreateConnectionAsync();
}
