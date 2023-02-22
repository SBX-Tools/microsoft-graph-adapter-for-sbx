using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.FilteredResults;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[ApiController]
[Route("v1.0/users")]
public class UserGraphController : GraphControllerBase
{
    public UserGraphController(GraphServiceClient graphServiceClient) : base(graphServiceClient)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return new FilteredUserResult(
            await GraphServiceClient.Users
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser([FromRoute] string userId)
    {
        return new FilteredUserResult(
            await GraphServiceClient.Users[userId]
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }
}