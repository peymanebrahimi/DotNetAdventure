using Webhook.Api.Models;

namespace Webhook.Api.Repositories;

public class InMemoryWebhookSubscriptionRepository
{
    private readonly List<WebhookSubscription> _subscriptions = [];
    
    public void Add(WebhookSubscription webhookSubscription)
    {
        _subscriptions.Add(webhookSubscription);
    }
    
    public IReadOnlyList<WebhookSubscription> GetByEventType(string eventType)
    {
        return _subscriptions.Where(x=>x.EventType == eventType).ToList().AsReadOnly();
    }
}