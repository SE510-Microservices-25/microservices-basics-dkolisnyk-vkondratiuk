using System.ComponentModel.DataAnnotations;

namespace Notifications.Api.Notifications.Payloads;

public sealed class DeleteNotificationPayload
{
    [Required]
    public Guid Id { get; set; }
}
