using System.ComponentModel.DataAnnotations;

namespace Subscriptions.Api.Subscriptions.Payloads;

public class DeleteSubscriptionPayload
{
    [Required] public Guid Id { get; set; }
}
