using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Configurations;

public class BacklogItemConfiguration : IEntityTypeConfiguration<GameBacklogItem>
{
    public void Configure(EntityTypeBuilder<GameBacklogItem> builder)
    {
        builder.OwnsOne(x => x.CompletionStatusDetails);
    }
}
