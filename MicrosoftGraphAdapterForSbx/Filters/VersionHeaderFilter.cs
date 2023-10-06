using Microsoft.AspNetCore.Mvc.Filters;

namespace MicrosoftGraphAdapterForSbx.Filters;

public class VersionHeaderFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        context.HttpContext.Response.Headers.Add("X-Graph-Adapter-Version", "1.0.1");
    }
}