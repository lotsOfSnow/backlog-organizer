namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;
public static class OrmMappings
{
    public static class Backlogs
    {
        public static readonly OrmValueMapping Table = "Backlogs";
    }

    public static class Groups
    {
        public static readonly OrmValueMapping Table = "Groups";

        public static class Assignments
        {
            public static readonly OrmValueMapping Table = "GroupAssignments";
        }
    }

    public static class Items
    {
        public static readonly OrmValueMapping Table = "Items";
    }
}
