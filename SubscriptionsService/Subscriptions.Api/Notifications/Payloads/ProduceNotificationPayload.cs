using System.ComponentModel.DataAnnotations;
using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Api.Notifications.Payloads;

public sealed class ProduceNotificationPayload
{
    [Required]
    public SubscribableTopics Topic { get; set; }
}
