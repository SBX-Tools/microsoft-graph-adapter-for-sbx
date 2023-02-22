using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredMailFolderResult : FilteredContentResult<MailFolder, UserMailFoldersCollectionResponse>
{
    public FilteredMailFolderResult(GraphResponse<MailFolder> response) : base(response)
    {
    }
    
    public FilteredMailFolderResult(GraphResponse<UserMailFoldersCollectionResponse> response) : base(response)
    {
    }

    public override CollectionResponse<MailFolder> GetCollectionResponse(UserMailFoldersCollectionResponse collection)
    {
        return new CollectionResponse<MailFolder>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override MailFolder Filter(MailFolder mailFolder)
    {
        return new MailFolder
        {
            AdditionalData = mailFolder.AdditionalData,
            Id = mailFolder.Id,
        };
    }
}