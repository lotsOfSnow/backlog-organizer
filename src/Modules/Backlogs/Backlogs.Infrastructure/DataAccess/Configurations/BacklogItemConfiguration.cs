using BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Configurations;

public class BacklogItemConfiguration : IEntityTypeConfiguration<BacklogItem>
{
    public void Configure(EntityTypeBuilder<BacklogItem> builder)
    {
        builder.ToTable(OrmMappings.Items.Table);

        builder.OwnsOne(x => x.CompletionStatusDetails);
    }
}
