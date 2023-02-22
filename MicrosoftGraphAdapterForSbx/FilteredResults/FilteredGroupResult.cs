using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredGroupResult : FilteredContentResult<Group, GraphServiceGroupsCollectionResponse>
{
    public FilteredGroupResult(GraphResponse<Group> response) : base(response)
    {
    }

    public FilteredGroupResult(GraphResponse<GraphServiceGroupsCollectionResponse> response) : base(response)
    {
    }

    public override CollectionResponse<Group> GetCollectionResponse(GraphServiceGroupsCollectionResponse collection)
    {
        return new CollectionResponse<Group>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override Group Filter(Group group)
    {
        return new Group
        {
            AdditionalData = group.AdditionalData,
            DisplayName = group.DisplayName,
            Id = group.Id,
            Mail = group.Mail,
            MailNickname = group.MailNickname,
        };
    }
}