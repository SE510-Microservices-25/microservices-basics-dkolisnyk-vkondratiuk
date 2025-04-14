using MediatR;
using Notifications.Business.Notifications.Core;
using Subscriptions.Business.Subscriptions.Core;

namespace Notifications.Business.Notifications.Commands;

public record CreateNotificationCommand(SubscribableTopics Topic, string Message) : IRequest<Guid>;

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Guid>
{
    private readonly INotificationRepository _repository;

    public CreateNotificationCommandHandler(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Topic = request.Topic,
            Message = request.Message
        };

        await _repository.Create(notification);

        return notification.Id;
    }
} 