using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public class FilteredMessageRuleResult : FilteredContentResult<MessageRule, MailFolderMessageRulesCollectionResponse>
{
    public FilteredMessageRuleResult(GraphResponse<MessageRule> response) : base(response)
    {
    }
    
    public FilteredMessageRuleResult(GraphResponse<MailFolderMessageRulesCollectionResponse> response) : base(response)
    {
    }

    public FilteredMessageRuleResult(GraphResponse response) : base(response)
    {
    }

    public override CollectionResponse<MessageRule> GetCollectionResponse(MailFolderMessageRulesCollectionResponse collection)
    {
        return new CollectionResponse<MessageRule>
        {
            Value = collection.Value,
            AdditionalData = collection.AdditionalData,
            NextLink = collection.NextLink
        };
    }

    public override MessageRule Filter(MessageRule messageRule)
    {
        return new MessageRule
        {
            AdditionalData = messageRule.AdditionalData,
            Actions = messageRule.Actions,
            Conditions = messageRule.Conditions,
            DisplayName = messageRule.DisplayName,
            Id = messageRule.Id,
            IsEnabled = messageRule.IsEnabled,
        };
    }
}