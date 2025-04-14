using MediatR;
using Notifications.Business.Notifications.Core;

namespace Notifications.Business.Notifications.Queries;

public record GetNotificationByIdQuery(Guid Id) : IRequest<Notification?>;

public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, Notification?>
{
    private readonly INotificationRepository _repository;

    public GetNotificationByIdQueryHandler(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Notification?> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetById(request.Id);
    }
} 