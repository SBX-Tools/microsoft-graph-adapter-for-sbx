using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredUserResult : FilteredContentResult<User, GraphServiceUsersCollectionResponse>
{
    public FilteredUserResult(GraphResponse<User> response) : base(response)
    {
    }
    
    public FilteredUserResult(GraphResponse<GraphServiceUsersCollectionResponse> response) : base(response)
    {
    }

    public override CollectionResponse<User> GetCollectionResponse(GraphServiceUsersCollectionResponse collection)
    {
        return new CollectionResponse<User>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override User Filter(User user)
    {
        return new User
        {
            AdditionalData = user.AdditionalData,
            DisplayName = user.DisplayName,
            Id = user.Id,
            Mail = user.Mail,
            MailboxSettings = user.MailboxSettings,
            UserPrincipalName = user.UserPrincipalName,
            ProxyAddresses = user.ProxyAddresses,
        };
    }
}