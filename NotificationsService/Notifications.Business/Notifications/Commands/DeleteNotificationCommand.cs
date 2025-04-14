using MediatR;

namespace Notifications.Business.Notifications.Commands;

public record DeleteNotificationCommand(Guid Id) : IRequest;

public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand>
{
    private readonly INotificationRepository _repository;

    public DeleteNotificationCommandHandler(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Id);
    }
} 