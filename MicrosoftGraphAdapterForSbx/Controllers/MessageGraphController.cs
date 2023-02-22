using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.FilteredResults;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[ApiController]
[Route("v1.0/users/{userId}/messages")]
public class MessageGraphController : GraphControllerBase
{
    public MessageGraphController(GraphServiceClient graphServiceClient) : base(graphServiceClient)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetMessages([FromRoute] string userId)
    {
        return new FilteredMessageResult(
            await GraphServiceClient.Users[userId].Messages
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpGet("{messageId}")]
    public async Task<IActionResult> GetMessage([FromRoute] string userId, [FromRoute] string messageId)
    {
        return new FilteredMessageResult(
            await GraphServiceClient.Users[userId].Messages[messageId]
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpPatch("{messageId}")]
    public async Task<IActionResult> PatchMessage([FromRoute] string userId, [FromRoute] string messageId, [FromBody] Message message)
    {
        return new FilteredMessageResult(
            await GraphServiceClient.Users[userId].Messages[messageId]
                .Request(GetRequestQueryString())
                .UpdateResponseAsync(new Message
                {
                    Categories = message.Categories,
                    IsRead = message.IsRead
                })
        );
    }

    [HttpPost("{messageId}/move")]
    [HttpPost("{messageId}/microsoft.graph.move")]
    public async Task<IActionResult> MoveMessage([FromRoute] string userId, [FromRoute] string messageId, [FromBody] MoveMessageRequest moveMessageRequest)
    {
        return new FilteredMessageResult(
            await GraphServiceClient.Users[userId].Messages[messageId].Move(moveMessageRequest.DestinationId)
                .Request(GetRequestQueryString())
                .PostResponseAsync()
        );
    }
}