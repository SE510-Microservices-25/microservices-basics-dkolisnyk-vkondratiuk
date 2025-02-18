using System.ComponentModel.DataAnnotations;
using Notifications.Business.Notifications.Core;

namespace Notifications.Api.Notifications.Payloads;

public sealed class CreateNotificationPayload
{
    [Required]
    public NotifiableTopics Topic { get; set; }

    public string? Message { get; set; }
}