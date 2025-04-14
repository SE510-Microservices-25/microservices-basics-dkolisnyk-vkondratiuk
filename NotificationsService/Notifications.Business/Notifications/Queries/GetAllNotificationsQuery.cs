using MediatR;
using Notifications.Business.Notifications.Core;

namespace Notifications.Business.Notifications.Queries;

public record GetAllNotificationsQuery : IRequest<IEnumerable<Notification>>;

public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, IEnumerable<Notification>>
{
    private readonly INotificationRepository _repository;

    public GetAllNotificationsQueryHandler(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.ListAll();
    }
} 