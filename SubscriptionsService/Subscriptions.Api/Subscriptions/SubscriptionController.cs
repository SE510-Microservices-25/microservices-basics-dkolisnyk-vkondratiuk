using Microsoft.AspNetCore.Mvc;
using Subscriptions.Api.Common.Responses;
using Subscriptions.Api.Subscriptions.Payloads;
using Subscriptions.Api.Subscriptions.Responses;
using Subscriptions.Business.Subscriptions;

namespace Subscriptions.Api.Subscriptions;

[ApiController]
[Route("api/subscriptions")]
public sealed class SubscriptionController
{
    private readonly SubscriptionsService _subscriptionsService;

    public SubscriptionController(SubscriptionsService subscriptionsService)
    {
        _subscriptionsService = subscriptionsService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CreateSubscriptionResponse), StatusCodes.Status201Created)]
    public async Task<IEnumerable<SubscriptionResponse>> ListAll()
    {
        var subscriptions = await _subscriptionsService.ListAll();

        var response = new List<SubscriptionResponse>();

        foreach (var subscription in subscriptions)
        {
            response.Add(
                new SubscriptionResponse
                {
                    Id = subscription.Id,
                    Topic = subscription.Topic,
                });
        }

        return response;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateSubscriptionResponse), StatusCodes.Status201Created)]
    public async Task<CreateSubscriptionResponse> Subscribe([FromBody] CreateSubscriptionPayload payload)
    {
        var id = await _subscriptionsService.Subscribe(payload.Topic);

        return new CreateSubscriptionResponse
        {
            Id = id,
        };
    }

    [HttpDelete]
    [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status200OK)]
    public async Task<StatusResponse> Unsubscribe([FromBody] DeleteSubscriptionPayload payload)
    {
        await _subscriptionsService.Unsubscribe(payload.Id);

        return new StatusResponse
        {
            Success = true,
        };
    }
}
