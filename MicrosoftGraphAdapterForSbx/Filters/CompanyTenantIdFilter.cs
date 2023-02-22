using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MicrosoftGraphAdapterForSbx.Filters;

public class CompanyTenantIdFilter : IAuthorizationFilter
{
    private readonly string _allowedTenantId;

    public CompanyTenantIdFilter(string allowedTenantId)
    {
        _allowedTenantId = allowedTenantId;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Company-Tenant-Id", out var tenantId) || tenantId != _allowedTenantId)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}