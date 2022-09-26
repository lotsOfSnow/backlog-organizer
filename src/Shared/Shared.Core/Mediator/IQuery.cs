using MediatR;

namespace BacklogOrganizer.Shared.Core.Mediator;
public interface IQuery<out TResult> : IRequest<TResult>
{
}
