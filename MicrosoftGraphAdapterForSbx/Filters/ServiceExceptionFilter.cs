using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Graph;

namespace MicrosoftGraphAdapterForSbx.Filters;

public class ServiceExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ServiceException serviceException)
        {
            context.Result = new ContentResult
            {
                StatusCode = (int)serviceException.StatusCode,
                Content = serviceException.RawResponseBody,
                ContentType = MediaTypeNames.Application.Json
            };
        }
    }
}