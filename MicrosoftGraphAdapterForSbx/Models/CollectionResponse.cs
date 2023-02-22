using System.Text.Json.Serialization;

namespace MicrosoftGraphAdapterForSbx.Models;

public class CollectionResponse<T>
{
    [JsonExtensionData]
    public IDictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    [JsonPropertyName("@odata.nextLink")]
    public string NextLink { get; set; } = string.Empty;
    public IEnumerable<T> Value { get; set; } = new List<T>();
}