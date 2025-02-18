using Microsoft.AspNetCore.Mvc;
using Notiffly.Api.Common.Responses;
using Notiffly.Api.Subscriptions.Payloads;

namespace Notiffly.Api.Subscriptions;

[ApiController]
[Route("api/subscriptions")]
public sealed class SubscriptionController
{
    [HttpPost]
    [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status201Created)]
    public async Task<StatusResponse> Subscribe([FromBody] SubscribePayload payload)
    {
        return new StatusResponse
        {
            Success = true,
        };
    }

    [HttpDelete]
    [ProducesResponseType(typeof(StatusResponse), StatusCodes.Status200OK)]
    public async Task<StatusResponse> Unsubscribe([FromBody] SubscribePayload payload)
    {
        return new StatusResponse
        {
            Success = true,
        };
    }
}
