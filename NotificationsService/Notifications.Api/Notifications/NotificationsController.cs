using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Business.Notifications.Commands;
using Notifications.Business.Notifications.Core;
using Notifications.Business.Notifications.Queries;

namespace Notifications.Api.Notifications;

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> GetAll()
    {
        var notifications = await _mediator.Send(new GetAllNotificationsQuery());
        return Ok(notifications);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Notification>> GetById(Guid id)
    {
        var notification = await _mediator.Send(new GetNotificationByIdQuery(id));
        if (notification == null)
        {
            return NotFound();
        }
        return Ok(notification);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNotificationCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteNotificationCommand(id));
        return NoContent();
    }
}
