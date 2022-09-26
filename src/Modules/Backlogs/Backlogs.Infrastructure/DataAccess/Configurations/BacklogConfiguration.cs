using BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Configurations;

public class BacklogConfiguration : IEntityTypeConfiguration<Backlog>
{
    public void Configure(EntityTypeBuilder<Backlog> builder)
    {
        builder.ToTable(OrmMappings.Backlogs.Table);
        const string groupsNavigation = "_groups";
        builder.HasMany<BacklogGroup>(groupsNavigation).WithOne();
        builder.Navigation(groupsNavigation).AutoInclude();
    }
}
