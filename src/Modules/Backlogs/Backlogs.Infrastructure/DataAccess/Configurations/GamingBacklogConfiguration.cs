using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Configurations;

public class GamingBacklogConfiguration : IEntityTypeConfiguration<GamingBacklog>
{
    public void Configure(EntityTypeBuilder<GamingBacklog> builder)
    {
        builder.OwnsMany<GameBacklogItemsGroup>("_groups", x =>
        {
            x.WithOwner().HasForeignKey(y => y.BacklogId);
            x.Property<Guid>("Id");
            x.HasKey("Id");
            x.OwnsMany<GroupAssignment>("_assignments", y =>
            {
                y.WithOwner(y => y.Group);
                y.HasOne(y => y.Item).WithMany();
                // TODO: In DBeaver, see what happens if there is no OwnsMany here and "items" are used directly.
            });
        });
    }
}
