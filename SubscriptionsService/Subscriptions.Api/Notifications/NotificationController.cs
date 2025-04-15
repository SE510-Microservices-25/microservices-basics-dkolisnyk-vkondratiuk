using Microsoft.AspNetCore.Mvc;
using Subscriptions.Api.Common.Responses;
using Subscriptions.Api.Notifications.Payloads;
using Subscriptions.Business.Notifications;

namespace Subscriptions.Api.Notifications;

[ApiController]
[Route("api/notifications")]
public class NotificationController
{
    private readonly INotificationProducer _notificationProducer;

    public NotificationController(INotificationProducer notificationProducer)
    {
        _notificationProducer = notificationProducer;
    }

    [HttpPost]
    [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status201Created)]
    public async Task<StatusResponse> Produce([FromBody] ProduceNotificationPayload payload)
    {
        await _notificationProducer.Produce(payload.Topic, new Guid());
        
        return new StatusResponse
        {
            Success = true,
        };
    }
}
