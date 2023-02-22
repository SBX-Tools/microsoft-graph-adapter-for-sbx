using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Models;

namespace MicrosoftGraphAdapterForSbx.FilteredResults;

public abstract class FilteredContentResult<T, TCollection> : IActionResult
{
    private readonly GraphResponse<T>? _response;
    private readonly GraphResponse<TCollection>? _responseCollection;
    private readonly GraphResponse? _emptyResponse;

    protected FilteredContentResult(GraphResponse<T> response)
    {
        _response = response;
    }

    protected FilteredContentResult(GraphResponse<TCollection> response)
    {
        _responseCollection = response;
    }

    protected FilteredContentResult(GraphResponse response)
    {
        _emptyResponse = response;
    }

    public abstract T Filter(T input);

    public abstract CollectionResponse<T> GetCollectionResponse(TCollection collection);

    public async Task ExecuteResultAsync(ActionContext context)
    {
        ActionResult result;
        if (_response != null)
        {
            result = new ObjectResult(Filter(await _response.GetResponseObjectAsync()))
            {
                StatusCode = (int)_response.StatusCode
            };
        }
        else if (_responseCollection != null)
        {
            var collectionResponse = GetCollectionResponse(await _responseCollection.GetResponseObjectAsync());
            collectionResponse.Value = collectionResponse.Value.Select(Filter);
            collectionResponse.NextLink = ReplaceNextLinkUri(collectionResponse.NextLink, context);
            if (collectionResponse.AdditionalData.TryGetValue("@odata.context", out var contextValue))
            {
                collectionResponse.AdditionalData["@odata.context"] = ReplaceNextLinkUri(contextValue.ToString(), context);
            }
            result = new ObjectResult(collectionResponse)
            {
                StatusCode = (int)_responseCollection.StatusCode
            };
        }
        else if (_emptyResponse != null)
        {
            result = new StatusCodeResult((int)_emptyResponse.StatusCode);
        }
        else
        {
            result = new BadRequestResult();
        }

        await result.ExecuteResultAsync(context);
    }

    private string? ReplaceNextLinkUri(string? nextLink, ActionContext context)
    {
        if (string.IsNullOrWhiteSpace(nextLink)) return null;
        var currentRequestUrl = context.HttpContext.Request.GetEncodedUrl();
        var pathVersionIndex = currentRequestUrl.LastIndexOf("/v1.0/", StringComparison.Ordinal);
        var nextLinkPathVersionIndex = nextLink.LastIndexOf("/v1.0/", StringComparison.Ordinal);
        if (pathVersionIndex < 0 || nextLinkPathVersionIndex < 0) return null;
        var baseUri = new Uri(currentRequestUrl[..(pathVersionIndex + 1)]);
        return new Uri(baseUri, nextLink[(nextLinkPathVersionIndex + 1)..]).ToString();
    }
}