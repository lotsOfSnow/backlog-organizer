using BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Configurations;

public class BacklogGroupConfiguration : IEntityTypeConfiguration<BacklogGroup>
{
    public void Configure(EntityTypeBuilder<BacklogGroup> builder)
    {
        builder.ToTable(OrmMappings.Groups.Table);

        builder.OwnsMany<GroupAssignment>("_assignments", assignments =>
        {
            assignments.ToTable(OrmMappings.GroupAssignments.Table);

            assignments.WithOwner().HasForeignKey(x => x.GroupId);
            assignments.HasOne<BacklogItem>().WithMany().HasForeignKey(x => x.ItemId);
            assignments.HasKey(x => new { x.GroupId, x.ItemId });
        });
    }
}
