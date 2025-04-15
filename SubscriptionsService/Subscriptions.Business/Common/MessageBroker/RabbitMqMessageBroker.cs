using MassTransit;

namespace Subscriptions.Business.Common.MessageBroker;

public class RabbitMqMessageBroker : IMessageBroker
{
    private readonly IBus _bus;

    public RabbitMqMessageBroker(IBus bus)
    {
        _bus = bus;
    }
    
    public async Task Publish<T>(T message) where T : class
    {
        await _bus.Publish<T>(message);
    }
}
