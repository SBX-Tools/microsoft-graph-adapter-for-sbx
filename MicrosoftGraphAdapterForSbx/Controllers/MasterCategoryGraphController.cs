using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.FilteredResults;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[ApiController]
[Route("v1.0/users/{userId}/outlook/masterCategories")]
public class MasterCategoryGraphController : GraphControllerBase
{
    public MasterCategoryGraphController(GraphServiceClient graphServiceClient) : base(graphServiceClient)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetMasterCategories([FromRoute] string userId)
    {
        return new FilteredMasterCategoryResult(
            await GraphServiceClient.Users[userId].Outlook.MasterCategories
                .Request(GetRequestQueryString())
                .GetResponseAsync()
        );
    }

    [HttpPost]
    public async Task<object> AddMasterCategory([FromRoute] string userId, [FromBody] OutlookCategory outlookCategory)
    {
        return new FilteredMasterCategoryResult(
            await GraphServiceClient.Users[userId].Outlook.MasterCategories
                .Request(GetRequestQueryString())
                .AddResponseAsync(new OutlookCategory
                {
                    DisplayName = outlookCategory.DisplayName,
                    Color = outlookCategory.Color,
                })
        );
    }
}