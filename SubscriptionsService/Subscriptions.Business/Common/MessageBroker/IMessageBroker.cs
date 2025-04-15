namespace Subscriptions.Business.Common.MessageBroker;

public interface IMessageBroker
{
    Task Publish<T>(T message) where T : class;
}
