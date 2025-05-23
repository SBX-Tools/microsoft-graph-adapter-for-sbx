using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace MicrosoftGraphAdapterForSbx.Controllers;

[Authorize(AuthenticationSchemes = "Bearer", Roles = "GraphAdapter.All")]
public class GraphControllerBase : ControllerBase
{
    protected readonly GraphServiceClient GraphServiceClient;

    public GraphControllerBase(GraphServiceClient graphServiceClient)
    {
        GraphServiceClient = graphServiceClient;
    }

    protected IEnumerable<Option> GetRequestQueryString()
    {
        return HttpContext.Request.Query.Select(x => new QueryOption(x.Key, x.Value));
    }
    
    [HttpGet("version")]
    public string GetGraphAdapterVersion()
    {
        return "1.0.2";
    }

}