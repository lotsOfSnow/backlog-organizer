using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetAllItems;

public record GetAllItemsQuery(Guid BacklogId) : IRequest<Result<IEnumerable<GameBacklogItemDto>>>;
