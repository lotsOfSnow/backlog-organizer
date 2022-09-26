namespace BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;

public record OrmValueMapping(string RawValue)
{
    public static implicit operator OrmValueMapping(string str)
        => new(str);

    public static implicit operator string(OrmValueMapping mapping)
        => mapping.RawValue;
}
