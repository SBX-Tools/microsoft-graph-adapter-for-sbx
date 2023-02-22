using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.FilteredResults;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[ApiController]
[Route("v1.0/subscriptions")]
public class SubscriptionGraphController : GraphControllerBase
{
    public SubscriptionGraphController(GraphServiceClient graphServiceClient) : base(graphServiceClient)
    {
    }

    [HttpPost]
    public async Task<IActionResult> AddSubscription([FromBody] Subscription subscription)
    {
        return new FilteredSubscriptionResult(
            await GraphServiceClient.Subscriptions
                .Request(GetRequestQueryString())
                .AddResponseAsync(subscription)
        );
    }

    [HttpPatch("{subscriptionId}")]
    public async Task<IActionResult> ExtendSubscription([FromRoute] string subscriptionId, [FromBody] Subscription subscription)
    {
        return new FilteredSubscriptionResult(
            await GraphServiceClient.Subscriptions[subscriptionId]
                .Request(GetRequestQueryString())
                .UpdateResponseAsync(new Subscription
                {
                    ExpirationDateTime = subscription.ExpirationDateTime
                })
        );
    }

    [HttpGet]
    public async Task<object> GetSubscriptions()
    {
        return new FilteredSubscriptionResult(
            await GraphServiceClient.Subscriptions
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpDelete("{subscriptionId}")]
    public async Task<object> DeleteSubscription([FromRoute] string subscriptionId)
    {
        return new FilteredSubscriptionResult(
            await GraphServiceClient.Subscriptions[subscriptionId]
                .Request(GetRequestQueryString())
                .DeleteResponseAsync()
        );
    }
}