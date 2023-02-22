using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredMessageResult : FilteredContentResult<Message, UserMessagesCollectionResponse>
{
    public FilteredMessageResult(GraphResponse<Message> response) : base(response)
    {
    }
    
    public FilteredMessageResult(GraphResponse<UserMessagesCollectionResponse> response) : base(response)
    {
    }

    public override CollectionResponse<Message> GetCollectionResponse(UserMessagesCollectionResponse collection)
    {
        return new CollectionResponse<Message>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override Message Filter(Message message)
    {
        return new Message
        {
            AdditionalData = message.AdditionalData,
            BccRecipients = message.BccRecipients,
            Categories = message.Categories,
            CcRecipients = message.CcRecipients,
            ChangeKey = message.ChangeKey,
            ConversationId = message.ConversationId,
            From = message.From,
            Id = message.Id,
            InternetMessageHeaders = message.InternetMessageHeaders,
            InternetMessageId = message.InternetMessageId,
            IsDraft = message.IsDraft,
            IsRead = message.IsRead,
            ParentFolderId = message.ParentFolderId,
            ReceivedDateTime = message.ReceivedDateTime,
            Sender = message.Sender,
            ToRecipients = message.ToRecipients,
        };
    }
}