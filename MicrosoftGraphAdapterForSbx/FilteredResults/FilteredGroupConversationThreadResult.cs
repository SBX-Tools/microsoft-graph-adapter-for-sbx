using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredGroupConversationThreadResult : FilteredContentResult<ConversationThread, ConversationThreadsCollectionResponse>
{
    // See https://learn.microsoft.com/en-us/office/client-developer/outlook/mapi/pidtaginternetmessageid-canonical-property
    private const string InternetMessageIdExtendedProperty = "String 0x1035";

    public FilteredGroupConversationThreadResult(GraphResponse<ConversationThreadsCollectionResponse> response) : base(response)
    {
    }

    public override CollectionResponse<ConversationThread> GetCollectionResponse(ConversationThreadsCollectionResponse collection)
    {
        return new CollectionResponse<ConversationThread>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override ConversationThread Filter(ConversationThread conversationThread)
    {
        return new ConversationThread
        {
            AdditionalData = conversationThread.AdditionalData,
            Id = conversationThread.Id,
            LastDeliveredDateTime = conversationThread.LastDeliveredDateTime,
            Posts = GetFilteredConversationThreadPosts(conversationThread)
        };
    }

    private ConversationThreadPostsCollectionPage GetFilteredConversationThreadPosts(ConversationThread conversationThread)
    {
        var threadPostsCollectionPage = new ConversationThreadPostsCollectionPage();
        foreach (var post in conversationThread.Posts)
        {
            threadPostsCollectionPage.Add(new Post
            {
                Id = post.Id,
                CreatedDateTime = post.CreatedDateTime,
                SingleValueExtendedProperties = new PostSingleValueExtendedPropertiesCollectionPage
                {
                    new()
                    {
                        Id = InternetMessageIdExtendedProperty,
                        Value = post.SingleValueExtendedProperties.FirstOrDefault(property => property.Id == InternetMessageIdExtendedProperty)?.Value
                    }
                }
            });
        }

        return threadPostsCollectionPage;
    }
}