using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.FilteredResults;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[ApiController]
[Route("v1.0/users/{userId}/mailFolders/inbox/messageRules")]
public class MessageRuleGraphController : GraphControllerBase
{
    public MessageRuleGraphController(GraphServiceClient graphServiceClient) : base(graphServiceClient)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetMessageRules([FromRoute] string userId)
    {
        return new FilteredMessageRuleResult(
            await GraphServiceClient.Users[userId].MailFolders.Inbox.MessageRules
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpGet("{messageRuleId}")]
    public async Task<IActionResult> GetMessageRule([FromRoute] string userId, [FromRoute] string messageRuleId)
    {
        return new FilteredMessageRuleResult(
            await GraphServiceClient.Users[userId].MailFolders.Inbox.MessageRules[messageRuleId]
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpPost]
    public async Task<object> AddMessageRule([FromRoute] string userId, [FromBody] MessageRule messageRule)
    {
        return new FilteredMessageRuleResult(
            await GraphServiceClient.Users[userId].MailFolders.Inbox.MessageRules
                .Request(GetRequestQueryString())
                .AddResponseAsync(new MessageRule
                {
                    Actions = new MessageRuleActions
                    {
                        MoveToFolder = messageRule.Actions?.MoveToFolder,
                        StopProcessingRules = false
                    },
                    Conditions = new MessageRulePredicates
                    {
                        FromAddresses = messageRule.Conditions?.FromAddresses,
                        HeaderContains = messageRule.Conditions?.HeaderContains,
                        SentToAddresses = messageRule.Conditions?.SentToAddresses
                    },
                    DisplayName = messageRule.DisplayName,
                    IsEnabled = messageRule.IsEnabled,
                    Sequence = messageRule.Sequence,
                })
        );
    }

    [HttpDelete("{messageRuleId}")]
    public async Task<object> DeleteMessageRule([FromRoute] string userId, [FromRoute] string messageRuleId)
    {
        return new FilteredMessageRuleResult(
            await GraphServiceClient.Users[userId].MailFolders.Inbox.MessageRules[messageRuleId]
                .Request(GetRequestQueryString())
                .DeleteResponseAsync()
        );
    }
}