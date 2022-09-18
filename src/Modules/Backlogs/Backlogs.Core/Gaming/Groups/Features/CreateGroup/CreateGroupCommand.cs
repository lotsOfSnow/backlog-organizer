using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.CreateGroup;

public record CreateGroupCommand(Guid BacklogId, string Name) : IRequest<Result<GameBacklogItemsGroupDto>>;
