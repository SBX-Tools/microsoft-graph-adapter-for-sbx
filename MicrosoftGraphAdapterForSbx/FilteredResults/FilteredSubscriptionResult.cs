using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredSubscriptionResult : FilteredContentResult<Subscription, GraphServiceSubscriptionsCollectionResponse>
{
    public FilteredSubscriptionResult(GraphResponse<Subscription> response) : base(response)
    {
    }
    
    public FilteredSubscriptionResult(GraphResponse<GraphServiceSubscriptionsCollectionResponse> response) : base(response)
    {
    }

    public FilteredSubscriptionResult(GraphResponse response) : base(response)
    {
    }

    public override CollectionResponse<Subscription> GetCollectionResponse(GraphServiceSubscriptionsCollectionResponse collection)
    {
        return new CollectionResponse<Subscription>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override Subscription Filter(Subscription subscription)
    {
        return new Subscription
        {
            AdditionalData = subscription.AdditionalData,
            ExpirationDateTime = subscription.ExpirationDateTime,
            Id = subscription.Id,
            Resource = subscription.Resource,
        };
    }
}