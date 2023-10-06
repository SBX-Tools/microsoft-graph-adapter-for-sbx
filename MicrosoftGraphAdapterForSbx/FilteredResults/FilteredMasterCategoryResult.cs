using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredMasterCategoryResult : FilteredContentResult<OutlookCategory, OutlookUserMasterCategoriesCollectionResponse>
{
    public FilteredMasterCategoryResult(GraphResponse<OutlookCategory> response) : base(response)
    {
    }
    
    public FilteredMasterCategoryResult(GraphResponse<OutlookUserMasterCategoriesCollectionResponse> response) : base(response)
    {
    }

    public FilteredMasterCategoryResult(GraphResponse response) : base(response)
    {
    }

    public override CollectionResponse<OutlookCategory> GetCollectionResponse(OutlookUserMasterCategoriesCollectionResponse collection)
    {
        return new CollectionResponse<OutlookCategory>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override OutlookCategory Filter(OutlookCategory messageRule)
    {
        return new OutlookCategory
        {
            AdditionalData = messageRule.AdditionalData,
            Color = messageRule.Color,
            DisplayName = messageRule.DisplayName,
            Id = messageRule.Id,
        };
    }
}