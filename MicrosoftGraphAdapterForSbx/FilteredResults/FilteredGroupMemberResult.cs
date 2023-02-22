using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredGroupMemberResult : FilteredContentResult<DirectoryObject, GroupMembersCollectionWithReferencesResponse>
{
    public FilteredGroupMemberResult(GraphResponse<GroupMembersCollectionWithReferencesResponse> response) : base(response)
    {
    }

    public override CollectionResponse<DirectoryObject> GetCollectionResponse(GroupMembersCollectionWithReferencesResponse collection)
    {
        return new CollectionResponse<DirectoryObject>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override DirectoryObject Filter(DirectoryObject member)
    {
        if (member is User user)
        {
            return new User
            {
                AdditionalData = member.AdditionalData,
                Id = user.Id,
                DisplayName = user.DisplayName,
                Mail = user.Mail,
                UserPrincipalName = user.UserPrincipalName,
            };
        }

        return new User();
    }
}