namespace Notifications.Business.Notifications.Core;

public class Notification
{
    public Guid Id { get; set; }
    public NotifiableTopics Topic { get; set; }
    public string? Message { get; set; }
}
