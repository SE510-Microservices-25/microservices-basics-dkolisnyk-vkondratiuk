using System.ComponentModel.DataAnnotations;
using Notiffly.Business.Subscriptions.Core;

namespace Notiffly.Api.Subscriptions.Payloads;

public sealed class CreateSubscriptionPayload
{
    [Required]
    public SubscribableTopics Topic { get; set; }
}
