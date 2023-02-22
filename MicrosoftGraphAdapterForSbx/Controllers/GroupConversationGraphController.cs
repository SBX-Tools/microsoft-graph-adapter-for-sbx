using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.FilteredResults;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[ApiController]
[Route("v1.0/groups")]
public class GroupConversationGraphController : GraphControllerBase
{
    public GroupConversationGraphController(GraphServiceClient graphServiceClient) : base(graphServiceClient)
    {
    }

    [HttpGet("{groupId}/conversations")]
    public async Task<IActionResult> GetGroupConversations([FromRoute] string groupId)
    {
        return new FilteredGroupConversationResult(
            await GraphServiceClient.Groups[groupId].Conversations
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpGet("{groupId}/conversations/{conversationId}/threads")]
    public async Task<IActionResult> GetGroupConversationThreads([FromRoute] string groupId, [FromRoute] string conversationId)
    {
        return new FilteredGroupConversationThreadResult(
            await GraphServiceClient.Groups[groupId].Conversations[conversationId].Threads
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }
}