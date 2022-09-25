namespace BacklogOrganizer.Shared.Core.Extensions;
public static class StringExtensions
{
    public static string InQuotationMarks(this string str)
        => $"\"{str}\"";
}
