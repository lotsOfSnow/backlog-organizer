using BacklogOrganizer.Shared.Core.Extensions;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;

public record OrmValueMapping(string RawValue)
{
    public string CaseSensitiveValue => RawValue.InQuotationMarks();

    public static implicit operator OrmValueMapping(string str)
        => new(str);

    public static implicit operator string(OrmValueMapping mapping)
        => mapping.RawValue;
}
