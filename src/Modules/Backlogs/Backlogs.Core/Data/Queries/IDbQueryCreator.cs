namespace BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
public interface IDbQueryCreator<TQuery>
    where TQuery : IDbQuery
{
    TQuery Create(string format, params string[] argumentsValues);
}
