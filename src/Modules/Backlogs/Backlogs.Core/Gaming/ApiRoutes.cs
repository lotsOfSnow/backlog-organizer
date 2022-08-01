namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;

public static class ApiRoutes
{
    private const string Base = "api";
    private const string Version = "v{version:apiVersion}";
    private const string VersionedBase = Base + "/" + Version;

    public const string GamingBacklogItems = VersionedBase + "/" + "gaming-backlog-items";
}
