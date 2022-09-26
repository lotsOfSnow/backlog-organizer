using System.Diagnostics;
using BacklogOrganizer.Shared.Core.Extensions;

namespace BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;

[DebuggerDisplay("{Value}")]
public record PostgresQuery
{
    public PostgresQuery(string query, params string[] argumentsValues)
    {
        var caseSensitiveValues = argumentsValues.Select(x => x.InQuotationMarks()).ToArray();
        Value = string.Format(query, caseSensitiveValues);
    }

    public string Value { get; }

    public static implicit operator string(PostgresQuery query)
        => query.Value;
}
