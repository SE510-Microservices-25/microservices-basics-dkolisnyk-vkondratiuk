using Notifications.Business.Notifications.Core;

namespace Notifications.Api.Notifications.Responses;

public sealed class NotificationResponse
{
    public Guid Id { get; set; }
    public NotifiableTopics Topic { get; set; }
    public string? Message { get; set; }
}
