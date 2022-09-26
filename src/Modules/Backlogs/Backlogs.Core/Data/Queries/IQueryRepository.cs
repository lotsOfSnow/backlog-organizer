namespace BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
public interface IQueryRepository
{
    IQueryDbConnectionFactory ConnectionFactory { get; }

    string GetQuery(string format, params string[] argumentValues);
}
