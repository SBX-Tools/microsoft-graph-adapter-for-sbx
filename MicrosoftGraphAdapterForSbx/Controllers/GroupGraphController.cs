using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.FilteredResults;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[ApiController]
[Route("v1.0/groups")]
public class GroupGraphController : GraphControllerBase
{
    public GroupGraphController(GraphServiceClient graphServiceClient) : base(graphServiceClient)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        return new FilteredGroupResult(
            await GraphServiceClient.Groups
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetGroup([FromRoute] string groupId)
    {
        return new FilteredGroupResult(
            await GraphServiceClient.Groups[groupId]
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }
}