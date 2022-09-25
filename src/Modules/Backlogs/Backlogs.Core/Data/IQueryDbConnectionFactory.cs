using System.Data;

namespace BacklogOrganizer.Modules.Backlogs.Core.Data;
public interface IQueryDbConnectionFactory
{
    Task<IDbConnection> CreateNewConnectionAsync();
}
