using Microsoft.AspNetCore.Mvc;
using Notifications.Api.Common.Responses;
using Notifications.Api.Notifications.Payloads;
using Notifications.Api.Notifications.Responses;
using Notifications.Business.Notifications;

namespace Notifications.Api.Notifications;

[ApiController]
[Route("api/notifications")]
public sealed class NotificationController : ControllerBase
{
    private readonly NotificationService _notificationService;

    public NotificationController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IEnumerable<NotificationResponse>> ListAll()
    {
        var notifications = await _notificationService.ListAll();
        return notifications.Select(n => new NotificationResponse
        {
            Id = n.Id,
            Topic = n.Topic,
            Message = n.Message
        });
    }

    [HttpPost]
    public async Task<CreateNotificationResponse> Create([FromBody] CreateNotificationPayload payload)
    {
        var id = await _notificationService.Create(payload.Topic, payload.Message);

        return new CreateNotificationResponse
        {
            Id = id
        };
    }

    [HttpDelete]
    public async Task<StatusResponse> Delete([FromBody] DeleteNotificationPayload payload)
    {
        await _notificationService.Delete(payload.Id);
        return new StatusResponse { Success = true };
    }
}
