using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredGroupConversationResult : FilteredContentResult<Conversation, GroupConversationsCollectionResponse>
{
    public FilteredGroupConversationResult(GraphResponse<GroupConversationsCollectionResponse> response) : base(response)
    {
    }

    public override CollectionResponse<Conversation> GetCollectionResponse(GroupConversationsCollectionResponse collection)
    {
        return new CollectionResponse<Conversation>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override Conversation Filter(Conversation conversation)
    {
        return new Conversation
        {
            AdditionalData = conversation.AdditionalData,
            Id = conversation.Id,
            LastDeliveredDateTime = conversation.LastDeliveredDateTime
        };
    }
}