namespace BacklogOrganizer.Shared.Core.Extensions;
public static class GuidExtensions
{
    public static string InQuotationMarks(this Guid guid)
        => guid.ToString().InQuotationMarks();
}
