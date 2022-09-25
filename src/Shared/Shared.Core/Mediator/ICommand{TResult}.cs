using MediatR;

namespace BacklogOrganizer.Shared.Core.Mediator;

public interface ICommand<out TResult> : IRequest<TResult>
{
}
