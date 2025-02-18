using System.ComponentModel.DataAnnotations;
using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Api.Subscriptions.Payloads;

public sealed class CreateSubscriptionPayload
{
    [Required]
    public SubscribableTopics Topic { get; set; }
}
