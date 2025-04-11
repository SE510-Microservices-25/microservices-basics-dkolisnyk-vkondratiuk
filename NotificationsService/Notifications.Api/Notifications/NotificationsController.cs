using Microsoft.AspNetCore.Mvc;
using Notifications.Api.Common.Responses;
using Notifications.Api.Notifications.Payloads;
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

    [HttpPost]
    public async Task<StatusResponse> Produce([FromBody] ProduceNotificationPayload payload)
    {
        await _notificationService.Produce(payload.Topic, payload.UserId);

        return new StatusResponse
        {
            Success = true,
        };
    }
}
