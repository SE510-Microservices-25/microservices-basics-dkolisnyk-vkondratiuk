using System.ComponentModel.DataAnnotations;
using Subscriptions.Business.Subscriptions.Core;

namespace Notifications.Api.Notifications.Payloads;

public sealed class ProduceNotificationPayload
{
    [Required]
    public SubscribableTopics Topic { get; set; }

    [Required] 
    public Guid UserId { get; set; }
}
