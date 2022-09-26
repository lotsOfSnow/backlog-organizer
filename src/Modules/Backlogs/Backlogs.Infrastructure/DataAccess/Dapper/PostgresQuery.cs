using System.Diagnostics;
using BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
using BacklogOrganizer.Shared.Core.Extensions;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Dapper;

[DebuggerDisplay("{Value}")]
public record PostgresQuery : IDbQuery
{
    public PostgresQuery(string format, params string[] argumentsValues)
    {
        var caseSensitiveValues = argumentsValues.Select(x => x.InQuotationMarks()).ToArray();
        Value = string.Format(format, caseSensitiveValues);
    }

    public string Value { get; }

    public static implicit operator string(PostgresQuery query)
        => query.Value;
}
