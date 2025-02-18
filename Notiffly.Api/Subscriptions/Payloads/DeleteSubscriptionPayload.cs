using System.ComponentModel.DataAnnotations;

namespace Notiffly.Api.Subscriptions.Payloads;

public class DeleteSubscriptionPayload
{
    [Required] public Guid Id { get; set; }
}
