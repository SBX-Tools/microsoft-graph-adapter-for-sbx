using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.FilteredResults;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[ApiController]
[Route("v1.0/users/{userId}/mailFolders")]
public class MailFolderGraphController : GraphControllerBase
{
    private const string SbxFolderDisplayName = "Sbx";

    public MailFolderGraphController(GraphServiceClient graphServiceClient) : base(graphServiceClient)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetMailFolders([FromRoute] string userId)
    {
        return new FilteredMailFolderResult(
            await GraphServiceClient.Users[userId].MailFolders
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpGet("{mailFolderId}")]
    public async Task<IActionResult> GetMailFolder([FromRoute] string userId, [FromRoute] string mailFolderId)
    {
        return new FilteredMailFolderResult(
            await GraphServiceClient.Users[userId].MailFolders[mailFolderId]
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpPost]
    public async Task<IActionResult> AddMailFolder([FromRoute] string userId, [FromBody] MailFolder mailFolder)
    {
        return new FilteredMailFolderResult(
            await GraphServiceClient.Users[userId].MailFolders
                .Request(GetRequestQueryString())
                .AddResponseAsync(new MailFolder
                {
                    DisplayName = SbxFolderDisplayName
                })
        );
    }
}