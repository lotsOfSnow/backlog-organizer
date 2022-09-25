using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Configurations;

public class GamingBacklogItemsGroupConfiguration : IEntityTypeConfiguration<GameBacklogItemsGroup>
{
    public void Configure(EntityTypeBuilder<GameBacklogItemsGroup> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.OwnsMany<GroupAssignment>("_assignments", assignments =>
        {
            assignments.WithOwner().HasForeignKey(x => x.GroupId);
            assignments.HasOne<BacklogItem>().WithMany().HasForeignKey(x => x.ItemId);
            assignments.HasKey(x => new { x.GroupId, x.ItemId });
        });
    }
}
