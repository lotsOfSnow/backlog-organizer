using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Configurations;

public class GamingBacklogConfiguration : IEntityTypeConfiguration<GamingBacklog>
{
    public void Configure(EntityTypeBuilder<GamingBacklog> builder)
    {
        const string groupsNavigation = "_groups";
        builder.HasMany<GameBacklogItemsGroup>(groupsNavigation).WithOne();
        builder.Navigation(groupsNavigation).AutoInclude();
    }
}
