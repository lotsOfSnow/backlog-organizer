using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetAllItems;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetDetails;
public record BacklogItemDetailsDto(Guid Id, string Name, IEnumerable<Guid> GroupsAssigned) : BacklogItemDto(Id, Name);
