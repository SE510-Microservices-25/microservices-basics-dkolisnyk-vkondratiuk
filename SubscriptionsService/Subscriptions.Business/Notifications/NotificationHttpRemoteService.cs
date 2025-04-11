using System.Text;
using System.Text.Json;
using Subscriptions.Business.Notifications.Payloads;
using Subscriptions.Business.Subscriptions.Core;

namespace Subscriptions.Business.Notifications;

public class NotificationHttpRemoteService : INotificationRemoteService
{
    private readonly HttpClient _httpClient;
    private readonly NotificationHttpRemoteServiceConfiguration _configuration;
    
    public NotificationHttpRemoteService(HttpClient httpClient, NotificationHttpRemoteServiceConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task Produce(SubscribableTopics topic, Guid userId)
    {
        var payload = new ProduceNotificationPayload
        {
            Topic = topic,
            UserId = userId,
        };
        
        var json = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        await _httpClient.PostAsync($"{_configuration.Host}/api/notifications", json);
    }
}
