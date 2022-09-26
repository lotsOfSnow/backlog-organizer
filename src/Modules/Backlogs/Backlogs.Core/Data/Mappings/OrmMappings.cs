namespace BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;
public static class OrmMappings
{
    public static class Backlogs
    {
        public static readonly OrmValueMapping Table = "Backlogs";

        public static class Columns
        {
            public static readonly OrmValueMapping Id = "Id";
        }
    }

    public static class Groups
    {
        public static readonly OrmValueMapping Table = "Groups";

        public static class Columns
        {
            public static readonly OrmValueMapping Id = "Id";
        }
    }

    public static class GroupAssignments
    {
        public static readonly OrmValueMapping Table = "GroupAssignments";

        public static class Columns
        {
            public static readonly OrmValueMapping GroupId = "GroupId";
        }
    }

    public static class Items
    {
        public static readonly OrmValueMapping Table = "Items";
    }
}
