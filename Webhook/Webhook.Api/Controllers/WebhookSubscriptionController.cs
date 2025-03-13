using Microsoft.AspNetCore.Mvc;
using Webhook.Api.Models;
using Webhook.Api.Repositories;

namespace Webhook.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WebhookSubscriptionController(InMemoryWebhookSubscriptionRepository webhookSubscriptionRepository)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(string eventType)
    {
        var webhookSubscriptions = webhookSubscriptionRepository.GetByEventType(eventType);

        return Ok(webhookSubscriptions);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateWebhookRequest request)
    {
        var webhookSubscription =
            new WebhookSubscription(Guid.NewGuid(), request.EventType, request.WebhookUrl, DateTime.UtcNow);

        webhookSubscriptionRepository.Add(webhookSubscription);

        return Ok(webhookSubscription);
    }
}