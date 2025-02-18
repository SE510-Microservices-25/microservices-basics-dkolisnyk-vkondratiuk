using System.ComponentModel.DataAnnotations;
using Notiffly.Api.Subscriptions.Core;

namespace Notiffly.Api.Subscriptions.Payloads;

public sealed class SubscribePayload
{
    [Required]
    public SubscribableTopics Topic { get; set; }
}
