using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.FilteredResults;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[ApiController]
[Route("v1.0/groups")]
public class GroupMemberGraphController : GraphControllerBase
{
    public GroupMemberGraphController(GraphServiceClient graphServiceClient) : base(graphServiceClient)
    {
    }

    [HttpGet("{groupId}/members")]
    public async Task<IActionResult> GetGroupMembers([FromRoute] string groupId)
    {
        return new FilteredGroupMemberResult(
            await GraphServiceClient.Groups[groupId].Members
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }
}